using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Mtsorella.Api.Common.Auth;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Identity;
using Mtsorella.Api.Domain.Inbox;
using Mtsorella.Api.Features.Identity;
using Mtsorella.Api.IntegrationTests.Infrastructure;
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.IntegrationTests;

// End-to-end auth against the running app + real PostgreSQL: admin logs in, turns a prihláška into an account,
// the member logs in with the temp password and is forced to change it, then logs in clean. Also asserts the
// admin endpoint's guards (401 without a token, 403 with a member token). The app issues and validates JWTs
// with the same per-process key, so tokens obtained over HTTP work against the same host.
public sealed class AuthFlowTests : IntegrationTestBase
{
    private const string AdminEmail = "admin@mtsorella.sk";
    private const string AdminPassword = "AdminPass123";

    public AuthFlowTests(PostgresContainerFixture fixture) : base(fixture)
    {
    }

    private async Task<ApplicationId> SeedAdminAndApplicationAsync()
    {
        var hasher = new PasswordHasherAdapter();
        await using var db = NewDbContext();

        var admin = UserAccount.Create(
            Email.Create(AdminEmail).Value, hasher.Hash(AdminPassword), Role.Admin, memberId: null,
            mustChangePassword: false).Value;
        db.UserAccounts.Add(admin);

        var application = Application.Submit(
            "Kid Name", new DateOfBirth(new DateOnly(2014, 5, 1)), MemberCategory.Juniors,
            "Parent Name", Email.Create("parent@example.com").Value, PhoneNumber.Create("+421900123456").Value,
            consentGiven: true, submittedOn: DateTimeOffset.UtcNow).Value;
        db.Applications.Add(application);

        await db.SaveChangesAsync();
        return application.Id;
    }

    private static async Task<Login.Response> LoginAsync(HttpClient client, string email, string password)
    {
        var response = await client.PostAsJsonAsync("/auth/login", new { email, password });
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Login.Response>())!;
    }

    private static void UseBearer(HttpClient client, string token) =>
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    [Fact]
    public async Task Admin_creates_account_from_application_member_logs_in_and_changes_password()
    {
        var applicationId = await SeedAdminAndApplicationAsync();
        await using var factory = new IntegrationWebAppFactory(ConnectionString);
        var adminClient = factory.CreateClient();

        var adminLogin = await LoginAsync(adminClient, AdminEmail, AdminPassword);
        UseBearer(adminClient, adminLogin.Token);

        var createResponse = await adminClient.PostAsJsonAsync(
            "/admin/accounts", new { applicationId = applicationId.Value, loginEmail = "kid@mtsorella.sk" });
        Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);
        var created = (await createResponse.Content.ReadFromJsonAsync<CreateMemberAccount.Response>())!;
        Assert.False(string.IsNullOrEmpty(created.TemporaryPassword));

        await using (var db = NewDbContext())
        {
            var application = await db.Applications.FindAsync(applicationId);
            Assert.Equal(ApplicationStatus.Accepted, application!.Status);
        }

        var memberClient = factory.CreateClient();
        var firstLogin = await LoginAsync(memberClient, created.Email, created.TemporaryPassword);
        Assert.True(firstLogin.MustChangePassword);

        UseBearer(memberClient, firstLogin.Token);
        var changeResponse = await memberClient.PostAsJsonAsync(
            "/auth/change-password", new { currentPassword = created.TemporaryPassword, newPassword = "NewMemberPass1" });
        Assert.Equal(HttpStatusCode.NoContent, changeResponse.StatusCode);

        var secondLogin = await LoginAsync(memberClient, created.Email, "NewMemberPass1");
        Assert.False(secondLogin.MustChangePassword);
    }

    [Fact]
    public async Task Admin_endpoint_returns_401_without_a_token_and_403_for_a_member()
    {
        var applicationId = await SeedAdminAndApplicationAsync();
        await using var factory = new IntegrationWebAppFactory(ConnectionString);
        var client = factory.CreateClient();

        var anonymous = await client.PostAsJsonAsync(
            "/admin/accounts", new { applicationId = applicationId.Value, loginEmail = "x@mtsorella.sk" });
        Assert.Equal(HttpStatusCode.Unauthorized, anonymous.StatusCode);

        var adminLogin = await LoginAsync(client, AdminEmail, AdminPassword);
        UseBearer(client, adminLogin.Token);
        var created = (await (await client.PostAsJsonAsync(
            "/admin/accounts", new { applicationId = applicationId.Value, loginEmail = "member@mtsorella.sk" }))
            .Content.ReadFromJsonAsync<CreateMemberAccount.Response>())!;

        var memberClient = factory.CreateClient();
        var memberLogin = await LoginAsync(memberClient, created.Email, created.TemporaryPassword);
        UseBearer(memberClient, memberLogin.Token);

        // Authorization runs before the handler, so the (already-accepted) application id is irrelevant here.
        var forbidden = await memberClient.PostAsJsonAsync(
            "/admin/accounts", new { applicationId = Guid.NewGuid(), loginEmail = "y@mtsorella.sk" });
        Assert.Equal(HttpStatusCode.Forbidden, forbidden.StatusCode);
    }
}
