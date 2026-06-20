using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Badges;

// Catalog of awardable badges (aggregate root). Admins manage the catalog and assign them (BE-20);
// members reference earned badges by BadgeId (D9).
public sealed class Badge : AggregateRoot<BadgeId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public MediaRef? Icon { get; private set; }
    public string? Criteria { get; private set; }
    public bool IsActive { get; private set; }

    private Badge(BadgeId id, string name, string description, MediaRef? icon, string? criteria)
    {
        Id = id;
        Name = name;
        Description = description;
        Icon = icon;
        Criteria = criteria;
        IsActive = true;
    }

    public static ErrorOr<Badge> Create(string name, string description, MediaRef? icon = null, string? criteria = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Error.Validation("Badge.NameRequired", "Badge name is required.");
        }

        if (string.IsNullOrWhiteSpace(description))
        {
            return Error.Validation("Badge.DescriptionRequired", "Badge description is required.");
        }

        return new Badge(BadgeId.New(), name.Trim(), description.Trim(), icon, criteria);
    }

    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
}
