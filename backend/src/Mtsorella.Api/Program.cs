using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using FluentValidation;
using Mediator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Common.Behaviors;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Persistence;
using Mtsorella.Api.Persistence.Outbox;
using Mtsorella.Api.Persistence.Repositories;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Structured logging (reads the "Serilog" section of appsettings, with sane code defaults).
builder.Services.AddSerilog((services, configuration) => configuration
    .ReadFrom.Configuration(builder.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .WriteTo.Console());

// The single shared database. The outbox interceptor turns aggregates' domain events into OutboxMessage
// rows within the same SaveChanges transaction (stateless, so one shared instance is fine).
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseNpgsql(builder.Configuration.GetConnectionString("Default"))
        .AddInterceptors(new ConvertDomainEventsToOutboxInterceptor()));

// Drains the outbox in the background, publishing each persisted event through Mediator.
builder.Services.AddHostedService<OutboxProcessor>();

// Repositories: the open generic covers simple cases; register a specific interface per
// aggregate when it needs its own query methods.
builder.Services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();

// Auth: own UserAccount + PasswordHasher + JWT bearer (not full ASP.NET Identity). The signing key comes
// from Jwt:Secret (env var Jwt__Secret / user-secrets); when unset we fall back to a random per-process key
// so the app still boots (dev/test/CI) — production MUST configure a stable secret (see backend/README.md).
var jwtSection = builder.Configuration.GetSection("Jwt");
byte[] signingKeyBytes = jwtSection["Secret"] is { Length: > 0 } secret
    ? Encoding.UTF8.GetBytes(secret)
    : RandomNumberGenerator.GetBytes(32);
bool usingEphemeralKey = string.IsNullOrEmpty(jwtSection["Secret"]);
var jwtSettings = new JwtSettings(
    issuer: jwtSection["Issuer"] ?? "mtsorella",
    audience: jwtSection["Audience"] ?? "mtsorella",
    accessTokenMinutes: jwtSection.GetValue<int?>("AccessTokenMinutes") ?? 60,
    signingKey: new SymmetricSecurityKey(signingKeyBytes));
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasherAdapter>();
builder.Services.AddSingleton<IJwtTokenIssuer, JwtTokenIssuer>();
builder.Services.AddScoped<ICurrentUser, CurrentUser>();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Keep our short claim names (sub/role/member_id) verbatim instead of the legacy URI remapping.
        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtSettings.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = jwtSettings.SigningKey,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromSeconds(30),
            NameClaimType = JwtRegisteredClaimNames.Sub,
            RoleClaimType = JwtTokenIssuer.RoleClaim,
        };
    });

// Two policies: "Admin" gates the admin panel; "Member" is any authenticated user (the member zone).
// Guest endpoints simply carry no RequireAuthorization.
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole(Role.Admin.ToString()));
    options.AddPolicy("Member", policy => policy.RequireAuthenticatedUser());
});

// Mediator (source-generated). Scoped so handlers can depend on the scoped DbContext.
builder.Services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);

// Validation runs as a pipeline behavior in front of every ErrorOr-returning handler.
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Auto-discover and register every vertical slice's endpoint.
builder.Services.AddEndpoints(typeof(Program).Assembly);

// Built-in OpenAPI document at /openapi/v1.json (consumed by the frontend client generator).
builder.Services.AddOpenApi();

var app = builder.Build();

if (usingEphemeralKey)
{
    app.Logger.LogWarning(
        "Jwt:Secret is not configured — using a random per-process signing key. Tokens will not survive a " +
        "restart or work across instances. Set Jwt__Secret in production.");
}

app.UseSerilogRequestLogging();

app.UseAuthentication();
app.UseAuthorization();

app.MapOpenApi();
app.MapScalarApiReference(); // interactive API docs at /scalar

app.MapEndpoints();

// Seed the first admin from configuration when provided (Admin__Email / Admin__Password, kept out of source).
// Idempotent and a no-op when unconfigured, so the unit-test host boots without touching the database.
if (builder.Configuration["Admin:Email"] is { Length: > 0 } adminEmail
    && builder.Configuration["Admin:Password"] is { Length: > 0 } adminPassword)
{
    using var scope = app.Services.CreateScope();
    await AdminSeeder.SeedAsync(
        scope.ServiceProvider.GetRequiredService<AppDbContext>(),
        scope.ServiceProvider.GetRequiredService<IPasswordHasher>(),
        adminEmail,
        adminPassword);
}

app.Run();

// Exposed so integration tests can boot the app with WebApplicationFactory<Program>.
public partial class Program;
