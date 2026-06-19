using ErrorOr;

namespace Mtsorella.Api.Domain.Common.ValueObjects;

// Computed challenge score: +10 completion, +5 on-time bonus, +0..20 quality (FR-M27 / FR-A6 / BE-17).
public sealed record ChallengeScore
{
    public const int CompletionPoints = 10;
    public const int OnTimeBonusPoints = 5;
    public const int MaxQuality = 20;

    public int Completion { get; }
    public int OnTimeBonus { get; }
    public int Quality { get; }

    private ChallengeScore(int completion, int onTimeBonus, int quality)
    {
        Completion = completion;
        OnTimeBonus = onTimeBonus;
        Quality = quality;
    }

    public int Total => Completion + OnTimeBonus + Quality;

    public static ErrorOr<ChallengeScore> Create(int quality, bool onTime)
    {
        if (quality is < 0 or > MaxQuality)
        {
            return Error.Validation(
                "ChallengeScore.QualityOutOfRange",
                $"Quality must be between 0 and {MaxQuality}.");
        }

        return new ChallengeScore(CompletionPoints, onTime ? OnTimeBonusPoints : 0, quality);
    }
}
