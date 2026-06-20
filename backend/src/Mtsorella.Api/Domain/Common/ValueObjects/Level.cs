namespace Mtsorella.Api.Domain.Common.ValueObjects;

// Level is DERIVED from Points via a fixed 6-rung ladder (prototype point ranges) — never stored.
// The human label (SK/CZ) is a frontend concern (D7); the domain exposes the neutral LevelName + Tier.
//   0–100 Beginner(Bronze) · 101–300 Improver(Bronze+) · 301–600 Advanced(Silver)
//   · 601–1000 Professional(Gold) · 1001–1500 Star(Gold+) · 1501+ Sorella(Diamond) — FR-M40/M41.
public sealed record Level
{
    public int Rung { get; }
    public LevelName Name { get; }
    public LevelTier Tier { get; }
    public int Min { get; }
    public int? Max { get; }

    private Level(int rung, LevelName name, LevelTier tier, int min, int? max)
    {
        Rung = rung;
        Name = name;
        Tier = tier;
        Min = min;
        Max = max;
    }

    private static readonly Level[] Ladder =
    [
        new(1, LevelName.Beginner,     LevelTier.Bronze,     0,    100),
        new(2, LevelName.Improver,     LevelTier.BronzePlus, 101,  300),
        new(3, LevelName.Advanced,     LevelTier.Silver,     301,  600),
        new(4, LevelName.Professional, LevelTier.Gold,       601,  1000),
        new(5, LevelName.Star,         LevelTier.GoldPlus,   1001, 1500),
        new(6, LevelName.Sorella,      LevelTier.Diamond,    1501, null)
    ];

    public static Level For(Points points)
    {
        int value = points.Value;
        foreach (Level level in Ladder)
        {
            if (value >= level.Min && (level.Max is null || value <= level.Max.Value))
            {
                return level;
            }
        }

        return Ladder[^1];
    }

    // Points still needed to reach the next rung (FR-M35), or null at the top rung.
    public int? PointsToNext(Points points) =>
        Max is null ? null : Max.Value + 1 - points.Value;
}
