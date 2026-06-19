using Mtsorella.Api.Domain.Common;
// Disambiguate from System.ApplicationId, which ImplicitUsings brings into scope via `using System;`.
using ApplicationId = Mtsorella.Api.Domain.Common.ApplicationId;

namespace Mtsorella.Api.Domain.Inbox;

public sealed record ApplicationSubmitted(ApplicationId ApplicationId) : IDomainEvent;
