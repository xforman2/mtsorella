using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Inbox;

// A submitted partnership/cooperation form (aggregate root) with a New → Handled lifecycle.
// FR-P32–P33 / BE-24.
public sealed class PartnershipInquiry : AggregateRoot<PartnershipInquiryId>
{
    public string CompanyName { get; private set; } = null!;
    public string ContactPerson { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public PhoneNumber Phone { get; private set; } = null!;
    public CooperationType CooperationType { get; private set; }
    public string Message { get; private set; } = null!;
    public InquiryStatus Status { get; private set; }
    public DateTimeOffset SubmittedOn { get; private set; }

    private PartnershipInquiry(
        PartnershipInquiryId id,
        string companyName,
        string contactPerson,
        Email email,
        PhoneNumber phone,
        CooperationType cooperationType,
        string message,
        DateTimeOffset submittedOn)
    {
        Id = id;
        CompanyName = companyName;
        ContactPerson = contactPerson;
        Email = email;
        Phone = phone;
        CooperationType = cooperationType;
        Message = message;
        Status = InquiryStatus.New;
        SubmittedOn = submittedOn;
    }

    // EF Core materialization ctor; EF populates mapped members after construction.
    private PartnershipInquiry()
    {
    }

    public static ErrorOr<PartnershipInquiry> Submit(
        string companyName,
        string contactPerson,
        Email email,
        PhoneNumber phone,
        CooperationType cooperationType,
        string message,
        DateTimeOffset submittedOn)
    {
        if (string.IsNullOrWhiteSpace(companyName))
        {
            return Error.Validation("PartnershipInquiry.CompanyNameRequired", "Company name is required.");
        }

        if (string.IsNullOrWhiteSpace(contactPerson))
        {
            return Error.Validation("PartnershipInquiry.ContactPersonRequired", "Contact person is required.");
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            return Error.Validation("PartnershipInquiry.MessageRequired", "Message is required.");
        }

        return new PartnershipInquiry(
            PartnershipInquiryId.New(), companyName.Trim(), contactPerson.Trim(),
            email, phone, cooperationType, message.Trim(), submittedOn);
    }

    public void MarkHandled() => Status = InquiryStatus.Handled;
}
