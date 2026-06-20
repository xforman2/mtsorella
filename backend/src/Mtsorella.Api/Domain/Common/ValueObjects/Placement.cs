namespace Mtsorella.Api.Domain.Common.ValueObjects;

// FR-P16 — a competition placement: an optional numeric rank plus a human label (e.g. 1 / "Finalist").
public sealed record Placement(int? Rank, string Label);
