# Changelog

All notable changes to this project are documented here, following
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added

- Backend CI via GitHub Actions (issue #14) — `.github/workflows/ci-backend.yml`, path-filtered to
  `backend/**`, runs restore (locked mode) → build → the full test suite (unit + Testcontainers
  PostgreSQL integration; `ubuntu-latest` ships Docker) on pushes to `main` and PRs. Coverage is
  collected with coverlet and rendered into the run summary (a Markdown table via ReportGenerator's
  `MarkdownSummaryGithub`), with the full HTML report uploaded as an artifact — no external service or
  secrets. Report-only (no gate yet); generated code and migrations are filtered out of the headline %.
- Test infrastructure for the persistence layer (issue #30) — a new
  `backend/tests/Mtsorella.Api.IntegrationTests` project running container-backed tests against real
  PostgreSQL via `Testcontainers.PostgreSql`: a shared-container fixture that applies the migrations and
  resets data between tests with Respawn, aggregate save/load round-trips (value objects, owned
  collections, the Challenge/Submission split), a migration-apply check, generic `Repository<,>` CRUD,
  and an outbox end-to-end test (row written on save, dispatched by the worker). Fast unit tests stay in
  `Mtsorella.Api.Tests`; outbox serialization coverage now spans every domain event. Coverage reporting
  via `coverlet` + `dotnet-reportgenerator-globaltool`.
- Domain-event dispatch via the outbox pattern (issue #29) — a `SaveChanges` interceptor persists
  each aggregate's domain events as `OutboxMessage` rows in the same transaction as the state change,
  and a background worker (`OutboxProcessor`) publishes them asynchronously through Mediator, retrying
  on failure (at-least-once). `IDomainEvent` now extends Mediator's `INotification`. Includes the
  `AddOutboxMessages` migration (the `OutboxMessages` table with a filtered index on unprocessed rows).
- Backend domain layer (`backend/src/Mtsorella.Api/Domain/`) — implements the `domain-model.md`
  design (issue #24): rich aggregates (Member, Coach, Badge, Training, Challenge,
  ChallengeSubmission, Announcement, TeamGoal, MonthlyHighlight, Achievement, Performance,
  GalleryPhoto, Sponsor, and the Inbox forms), value objects (Email, PhoneNumber, Points, Level,
  Streak, ChallengeScore, …), strongly-typed ids, and domain events that are raised and held (not
  yet dispatched). Construction/mutation go through `ErrorOr` factories; no persistence. Covered by
  comprehensive xUnit unit tests.
- Domain model design (`domain-model.md`) — the full MT Sorella domain (aggregates, value objects,
  enums, relationships, domain events) derived from `requirements.md` and the prototype, with each
  model linking back to its requirements source. Design/plan for implementing the backend `Domain/`
  layer; no persistence (issue #24).
- Frontend scaffold under `/frontend` — React + Vite + TypeScript, react-router skeleton
  (public / member / admin zones), CSS Modules with the MT Sorella design tokens, Vitest +
  React Testing Library, ESLint + Prettier, and a thin typed `fetch` API client.
- Repo-wide config: root `.gitignore` (Node + .NET), `.editorconfig`, and this changelog.
- Functional requirements (`requirements.md`) and the high-fidelity prototype (`/prototype`).
- Issue tracking + 7 Claude Code skills under `.claude/skills/`.

### Fixed

- `Recurrence.Days` could not be persisted (issue #30) — it was an `IReadOnlySet<DayOfWeek>`, which EF
  Core's change tracker cannot snapshot even inside a JSON column, so saving any `Training` threw. Changed
  to an ordered `IReadOnlyList<DayOfWeek>`; the JSON shape and `jsonb` column are unchanged (no migration).
  Surfaced by the new integration round-trip test.

### Removed

- Placeholder `Product` vertical — the sample entity, repository, `/products` feature slices,
  `AppDbContext` mapping, DI registration, and the initial EF migration/snapshot. Throwaway scaffold
  that is no longer needed; real persistence arrives with its own issue.
