using ErrorOr;
using Mtsorella.Api.Domain.Common;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Domain.Coaches;

// Coaching staff (aggregate root). Only the public-profile subset lives here; the coach↔admin login
// account is an auth boundary (§8, D10). ShowOnWebsite gates public visibility (FR-A5 / FR-P18).
// FR-P18–P20 / BE-7.
public sealed class Coach : AggregateRoot<CoachId>
{
    public string FullName { get; private set; }
    public string RoleTitle { get; private set; }
    public int YearsInTeam { get; private set; }
    public string Bio { get; private set; }
    public MediaRef? Photo { get; private set; }
    public bool ShowOnWebsite { get; private set; }

    private Coach(CoachId id, string fullName, string roleTitle, int yearsInTeam, string bio, MediaRef? photo, bool showOnWebsite)
    {
        Id = id;
        FullName = fullName;
        RoleTitle = roleTitle;
        YearsInTeam = yearsInTeam;
        Bio = bio;
        Photo = photo;
        ShowOnWebsite = showOnWebsite;
    }

    public static ErrorOr<Coach> Create(
        string fullName,
        string roleTitle,
        int yearsInTeam,
        string bio,
        MediaRef? photo = null,
        bool showOnWebsite = false)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Error.Validation("Coach.FullNameRequired", "Full name is required.");
        }

        if (string.IsNullOrWhiteSpace(roleTitle))
        {
            return Error.Validation("Coach.RoleTitleRequired", "Role title is required.");
        }

        if (yearsInTeam < 0)
        {
            return Error.Validation("Coach.YearsInTeamNegative", "Years in team cannot be negative.");
        }

        return new Coach(CoachId.New(), fullName.Trim(), roleTitle.Trim(), yearsInTeam, bio?.Trim() ?? string.Empty, photo, showOnWebsite);
    }

    public ErrorOr<Updated> Edit(string fullName, string roleTitle, int yearsInTeam, string bio, MediaRef? photo)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return Error.Validation("Coach.FullNameRequired", "Full name is required.");
        }

        if (string.IsNullOrWhiteSpace(roleTitle))
        {
            return Error.Validation("Coach.RoleTitleRequired", "Role title is required.");
        }

        if (yearsInTeam < 0)
        {
            return Error.Validation("Coach.YearsInTeamNegative", "Years in team cannot be negative.");
        }

        FullName = fullName.Trim();
        RoleTitle = roleTitle.Trim();
        YearsInTeam = yearsInTeam;
        Bio = bio?.Trim() ?? string.Empty;
        Photo = photo;
        return Result.Updated;
    }

    public void Show() => ShowOnWebsite = true;

    public void Hide() => ShowOnWebsite = false;
}
