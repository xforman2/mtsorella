using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.TeamGoals;

public sealed record TeamGoalCompleted(TeamGoalId TeamGoalId) : IDomainEvent;
