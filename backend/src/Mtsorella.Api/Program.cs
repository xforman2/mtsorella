using FluentValidation;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Mtsorella.Api.Common.Behaviors;
using Mtsorella.Api.Common.Endpoints;
using Mtsorella.Api.Persistence;
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

// The single shared database.
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// Repositories: the open generic covers simple cases; register a specific interface per
// aggregate when it needs its own query methods.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

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

app.UseSerilogRequestLogging();

app.MapOpenApi();
app.MapScalarApiReference(); // interactive API docs at /scalar

app.MapEndpoints();

app.Run();

// Exposed so integration tests can boot the app with WebApplicationFactory<Program>.
public partial class Program;
