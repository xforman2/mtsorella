using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Sponsors;

// A partner/sponsor shown on the site (aggregate root). FR-P24–P25 / FR-A13 / BE-12.
public sealed class Sponsor : AggregateRoot<SponsorId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public MediaRef? Logo { get; private set; }
    public string? WebsiteUrl { get; private set; }

    private Sponsor(SponsorId id, string name, string description, MediaRef? logo, string? websiteUrl)
    {
        Id = id;
        Name = name;
        Description = description;
        Logo = logo;
        WebsiteUrl = websiteUrl;
    }

    public static ErrorOr<Sponsor> Create(string name, string description, MediaRef? logo = null, string? websiteUrl = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("Sponsor.NameRequired", "Sponsor name is required.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Error.Validation("Sponsor.DescriptionRequired", "Sponsor description is required.");
        }

        return new Sponsor(SponsorId.New(), name.Trim(), description.Trim(), logo, websiteUrl);
    }
}
