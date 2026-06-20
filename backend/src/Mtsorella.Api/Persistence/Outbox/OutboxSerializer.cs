using System.Text.Json;

namespace Mtsorella.Api.Persistence.Outbox;

// Shared System.Text.Json settings used to write and read OutboxMessage.Payload, so the interceptor
// and the processor always agree on the wire format. Defaults round-trip every current event payload
// (enums, ints, DateTimeOffset, and the readonly record struct ids via their public ctor).
internal static class OutboxSerializer
{
    public static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);
}
