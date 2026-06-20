namespace Mtsorella.Api.Domain.Inbox;

// Lifecycle for the simple public forms (partnership inquiry, contact message): New → Handled.
public enum InquiryStatus
{
    New,
    Handled
}
