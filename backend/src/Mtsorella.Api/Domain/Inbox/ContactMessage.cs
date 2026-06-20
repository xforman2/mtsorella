using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Inbox;

// A submitted contact form (aggregate root) with a New → Handled lifecycle. FR-P26 / BE-24.
public sealed class ContactMessage : AggregateRoot<ContactMessageId>
{
    public string Name { get; private set; }
    public Email Email { get; private set; }
    public string Message { get; private set; }
    public InquiryStatus Status { get; private set; }
    public DateTimeOffset SubmittedOn { get; private set; }

    private ContactMessage(ContactMessageId id, string name, Email email, string message, DateTimeOffset submittedOn)
    {
        Id = id;
        Name = name;
        Email = email;
        Message = message;
        Status = InquiryStatus.New;
        SubmittedOn = submittedOn;
    }

    public static ErrorOr<ContactMessage> Submit(string name, Email email, string message, DateTimeOffset submittedOn)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("ContactMessage.NameRequired", "Name is required.");
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            return Error.Validation("ContactMessage.MessageRequired", "Message is required.");
        }

        return new ContactMessage(ContactMessageId.New(), name.Trim(), email, message.Trim(), submittedOn);
    }

    public void MarkHandled() => Status = InquiryStatus.Handled;
}
