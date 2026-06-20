using Mtsorella.Api.Domain.Common;

namespace Mtsorella.Api.Domain.Challenges;

public sealed record ChallengeCreated(ChallengeId ChallengeId, CoachId CreatedBy) : IDomainEvent;

public sealed record ChallengeSubmissionCreated(ChallengeSubmissionId SubmissionId, ChallengeId ChallengeId, MemberId MemberId) : IDomainEvent;

public sealed record ChallengeSubmissionReviewed(ChallengeSubmissionId SubmissionId, ChallengeId ChallengeId, MemberId MemberId, int AwardedPoints) : IDomainEvent;
