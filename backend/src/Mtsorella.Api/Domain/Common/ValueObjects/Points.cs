using ErrorOr;

namespace Mtsorella.Api.Domain.Common.ValueObjects;

// Non-negative score balance — the single source of truth for "how many points" (FR-M40).
public readonly record struct Points
{
    public int Value { get; }

    private Points(int value) => Value = value;

    public static Points Zero => new(0);

    public static ErrorOr<Points> From(int value) =>
        value < 0
            ? Error.Validation("Points.Negative", "Points cannot be negative.")
            : new Points(value);

    // Never drops below zero (D1) — a negative adjustment clamps at Zero rather than throwing.
    public Points Add(int delta) => new(Math.Max(0, Value + delta));
}
