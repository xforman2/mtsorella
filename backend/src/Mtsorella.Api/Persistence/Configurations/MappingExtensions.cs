using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mtsorella.Api.Domain.Common.ValueObjects;

namespace Mtsorella.Api.Persistence.Configurations;

// Shared mapping helpers. MediaRef is the same value object on ~10 aggregates, so map it one way here
// rather than repeating the column setup. Required vs optional is inferred from the navigation's CLR
// nullability (a non-nullable MediaRef is a required owned reference; MediaRef? is optional).
internal static class MappingExtensions
{
    public static void OwnsMediaRef<TOwner>(
        this EntityTypeBuilder<TOwner> builder,
        Expression<Func<TOwner, MediaRef?>> navigation)
        where TOwner : class
    {
        builder.OwnsOne(navigation, media =>
        {
            media.Property(m => m.StorageKey).HasMaxLength(512);
            media.Property(m => m.AltText).HasMaxLength(256);
        });
    }
}
