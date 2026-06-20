namespace Mtsorella.Api.Domain.Common.ValueObjects;

// A reference to stored media. The bytes/storage live behind a boundary (BE-21/22); the domain only
// points at them by StorageKey (D8).
public sealed record MediaRef(MediaKind Kind, string StorageKey, string? AltText = null);

public enum MediaKind
{
    Image,
    Video
}
