using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
using Mtsorella.Api.Domain.Inbox;

namespace Mtsorella.Api.Tests.Domain.Inbox;

public class InboxFormsTests
{
    private static (Email Email, PhoneNumber Phone) ValidContact()
    {
        return (Email.Create("contact@example.com").Value, PhoneNumber.Create("+421900123456").Value);
    }

    [Fact]
    public void PartnershipInquiry_submit_valid_starts_new_and_can_be_handled()
    {
        var (email, phone) = ValidContact();

        var result = PartnershipInquiry.Submit(
            "Acme", "John", email, phone, CooperationType.Financial, "Let's talk", DateTimeOffset.UtcNow);

        Assert.False(result.IsError);
        Assert.Equal(InquiryStatus.New, result.Value.Status);

        result.Value.MarkHandled();
        Assert.Equal(InquiryStatus.Handled, result.Value.Status);
    }

    [Fact]
    public void PartnershipInquiry_rejects_blank_company()
    {
        var (email, phone) = ValidContact();

        var result = PartnershipInquiry.Submit(
            " ", "John", email, phone, CooperationType.Other, "msg", DateTimeOffset.UtcNow);

        Assert.True(result.IsError);
    }

    [Fact]
    public void ContactMessage_submit_valid_succeeds()
    {
        var email = Email.Create("contact@example.com").Value;

        var result = ContactMessage.Submit("Jane", email, "Hello there", DateTimeOffset.UtcNow);

        Assert.False(result.IsError);
        Assert.Equal(InquiryStatus.New, result.Value.Status);
    }

    [Fact]
    public void ContactMessage_rejects_blank_message()
    {
        var email = Email.Create("contact@example.com").Value;

        var result = ContactMessage.Submit("Jane", email, " ", DateTimeOffset.UtcNow);

        Assert.True(result.IsError);
    }
}
