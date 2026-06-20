using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Domain.Inbox;

// A submitted child application form (aggregate root) with a New → Reviewed → Accepted/Rejected
// lifecycle. An accepted application is the seed for an admin creating a Member (FR-A4) — a future
// use case, not an automatic domain link. FR-P28–P31 / BE-24 / BE-28 (GDPR consent).
public sealed class Application : AggregateRoot<ApplicationId>
{
    public string ChildName { get; private set; }
    public DateOfBirth ChildDateOfBirth { get; private set; }
    public MemberCategory CategoryOfInterest { get; private set; }
    public string ParentName { get; private set; }
    public Email ParentEmail { get; private set; }
    public PhoneNumber ParentPhone { get; private set; }
    public string? PreviousExperience { get; private set; }
    public bool ConsentGiven { get; private set; }
    public ApplicationStatus Status { get; private set; }
    public DateTimeOffset SubmittedOn { get; private set; }

    private Application(
        ApplicationId id,
        string childName,
        DateOfBirth childDateOfBirth,
        MemberCategory categoryOfInterest,
        string parentName,
        Email parentEmail,
        PhoneNumber parentPhone,
        string? previousExperience,
        bool consentGiven,
        DateTimeOffset submittedOn)
    {
        Id = id;
        ChildName = childName;
        ChildDateOfBirth = childDateOfBirth;
        CategoryOfInterest = categoryOfInterest;
        ParentName = parentName;
        ParentEmail = parentEmail;
        ParentPhone = parentPhone;
        PreviousExperience = previousExperience;
        ConsentGiven = consentGiven;
        Status = ApplicationStatus.New;
        SubmittedOn = submittedOn;
    }

    public static ErrorOr<Application> Submit(
        string childName,
        DateOfBirth childDateOfBirth,
        MemberCategory categoryOfInterest,
        string parentName,
        Email parentEmail,
        PhoneNumber parentPhone,
        bool consentGiven,
        DateTimeOffset submittedOn,
        string? previousExperience = null)
    {
        if (string.IsNullOrWhiteSpace(childName))
        {
            return Error.Validation("Application.ChildNameRequired", "Child name is required.");
        }

        if (string.IsNullOrWhiteSpace(parentName))
        {
            return Error.Validation("Application.ParentNameRequired", "Parent name is required.");
        }

        if (!consentGiven)
        {
            return Error.Validation("Application.ConsentRequired", "Consent to data processing is required.");
        }

        var application = new Application(
            ApplicationId.New(), childName.Trim(), childDateOfBirth, categoryOfInterest,
            parentName.Trim(), parentEmail, parentPhone, previousExperience, consentGiven, submittedOn);
        application.RaiseDomainEvent(new ApplicationSubmitted(application.Id));
        return application;
    }

    public void MarkReviewed() => Status = ApplicationStatus.Reviewed;

    public void Accept() => Status = ApplicationStatus.Accepted;

    public void Reject() => Status = ApplicationStatus.Rejected;
}

public enum ApplicationStatus
{
    New,
    Reviewed,
    Accepted,
    Rejected
}
