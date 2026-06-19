using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Inbox;

namespace Mtsorella.Api.Tests.Domain.Inbox;

public class ApplicationTests
{
    private static (Email Email, PhoneNumber Phone, DateOfBirth Dob) ValidContact()
    {
        var email = Email.Create("parent@example.com").Value;
        var phone = PhoneNumber.Create("+421900123456").Value;
        var dob = new DateOfBirth(new DateOnly(2015, 5, 1));
        return (email, phone, dob);
    }

    [Fact]
    public void Submit_without_consent_is_error()
    {
        var (email, phone, dob) = ValidContact();

        var result = Application.Submit(
            "Child", dob, MemberCategory.Juniors, "Parent", email, phone,
            consentGiven: false, DateTimeOffset.UtcNow);

        Assert.True(result.IsError);
        Assert.Equal("Application.ConsentRequired", result.FirstError.Code);
    }

    [Fact]
    public void Submit_with_consent_succeeds_starts_new_and_raises_event()
    {
        var (email, phone, dob) = ValidContact();

        var result = Application.Submit(
            "Child", dob, MemberCategory.Juniors, "Parent", email, phone,
            consentGiven: true, DateTimeOffset.UtcNow);

        Assert.False(result.IsError);
        Assert.Equal(ApplicationStatus.New, result.Value.Status);
        Assert.Contains(result.Value.DomainEvents, domainEvent => domainEvent is ApplicationSubmitted);
    }

    [Fact]
    public void Accept_transitions_status()
    {
        var (email, phone, dob) = ValidContact();
        var application = Application.Submit(
            "Child", dob, MemberCategory.Juniors, "Parent", email, phone,
            consentGiven: true, DateTimeOffset.UtcNow).Value;

        application.Accept();

        Assert.Equal(ApplicationStatus.Accepted, application.Status);
    }
}
