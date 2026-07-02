using Microsoft.AspNetCore.Identity;
using Mtsorella.Api.Common.Auth;

namespace Mtsorella.Api.Tests.Common.Auth;

public class PasswordHasherAdapterTests
{
    private readonly PasswordHasherAdapter _hasher = new();

    [Fact]
    public void Hash_is_not_plaintext_and_verifies()
    {
        var hash = _hasher.Hash("s3cret-password");

        Assert.NotEqual("s3cret-password", hash);
        Assert.Equal(PasswordVerificationResult.Success, _hasher.Verify(hash, "s3cret-password"));
    }

    [Fact]
    public void Wrong_password_fails()
    {
        var hash = _hasher.Hash("s3cret-password");

        Assert.Equal(PasswordVerificationResult.Failed, _hasher.Verify(hash, "not-it"));
    }
}
