---
name: test-suite
description: Generate and run tests for the mtsorella monorepo — xUnit for the .NET backend and Vitest + React Testing Library for the React frontend. Use when adding tests or running the suites.
---

# test-suite

Conventions and commands for testing both stacks. Use when writing tests or running them locally
or in CI (see [[gh-actions-ci]]).

## Backend — xUnit (see [[dotnet-backend]])
Two projects, by speed and dependencies:

- **`backend/tests/Mtsorella.Api.Tests/`** — fast unit tests, no infrastructure. Mirrors `src/`. Domain
  and value-object logic, EF converters, outbox serialization, and host-based tests that don't touch the
  database (SQLite stands in for the relational SaveChanges pipeline where one is needed). Runs everywhere.
- **`backend/tests/Mtsorella.Api.IntegrationTests/`** — container-backed tests that **need a running
  Docker daemon**. Real-PostgreSQL mapping round-trips, migration apply, and the outbox end-to-end.

Keep new tests in the project that matches their cost: don't put a Testcontainers test in the unit project,
and don't reach for a container when SQLite or a pure unit test suffices.

### Unit / host tests
Plain xUnit for logic. Boot the API in-memory with `WebApplicationFactory<Program>` only for endpoints that
don't hit the DB (requires `public partial class Program;` in `Program.cs`):

```csharp
public class HealthEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Health_returns_ok()
    {
        var client = factory.CreateClient();
        var res = await client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }
}
```
Package: `Microsoft.AspNetCore.Mvc.Testing`.

### Integration tests (Testcontainers + PostgreSQL)
Packages: `Testcontainers.PostgreSql`, `Respawn`, `Microsoft.AspNetCore.Mvc.Testing`. The harness lives in
`Infrastructure/`: a `PostgresContainerFixture` starts one container for the whole assembly, applies the
migrations with `Database.MigrateAsync()` (deliberately Migrate, not EnsureCreated, so migrations are
exercised), and exposes a Respawn checkpoint. `IntegrationTestBase` joins the shared-container `[Collection]`
and resets data before each test; tests run sequentially within the collection, so resets stay race-free.

```csharp
public sealed class AggregateRoundTripTests(PostgresContainerFixture fixture) : IntegrationTestBase(fixture)
{
    [Fact]
    public async Task Member_round_trips()
    {
        var member = Member.Create(/* ... */).Value;
        await using (var db = NewDbContext()) { db.Members.Add(member); await db.SaveChangesAsync(); }
        await using (var db = NewDbContext())
            Assert.Equal(/* expected */, (await db.Members.SingleAsync(m => m.Id == member.Id)).Points.Value);
    }
}
```

Use a fresh `NewDbContext()` per save/load step so a value really hits the database. For the outbox worker,
boot the app with `IntegrationWebAppFactory` (overrides the `Default` connection string to the container) and
poll for `ProcessedOnUtc` — the `OutboxProcessor` dispatches on an interval, not instantly.

> Docker daemon required. If image pulls fail with a registry `unauthorized` error, stale tokens in
> `~/.docker/config.json` are likely being sent; run with `TESTCONTAINERS_RYUK_DISABLED=true` (the fixture
> disposes its own container, so cleanup still happens).

Run both: `dotnet test backend/Mtsorella.slnx`. Run only the fast suite:
`dotnet test backend/tests/Mtsorella.Api.Tests/Mtsorella.Api.Tests.csproj`.

### Coverage
```bash
dotnet test backend/Mtsorella.slnx --collect:"XPlat Code Coverage" --results-directory backend/TestResults
dotnet reportgenerator -reports:"backend/TestResults/**/coverage.cobertura.xml" \
  -targetdir:backend/TestResults/coverage-report -reporttypes:"TextSummary;Html" \
  -filefilters:"-**/Migrations/*" -classfilters:"-Mediator.*;-Microsoft.*;-System.*"
```
`coverlet.collector` is already referenced; `dotnet-reportgenerator-globaltool` is in the tools manifest
(`dotnet tool restore`). The class filters drop generated Mediator/OpenAPI/framework code so the number
reflects our own code. `TestResults/` is git-ignored.

## Frontend — Vitest + React Testing Library (see [[react-frontend]])
Setup (once, in `/frontend`):
```bash
npm i -D vitest @testing-library/react @testing-library/jest-dom @testing-library/user-event jsdom
```
`vite.config.ts`: `test: { environment: 'jsdom', setupFiles: './src/test/setup.ts', globals: true }`
and add `"test": "vitest"` to `package.json` scripts.

```tsx
import { render, screen } from '@testing-library/react';
import { Greeting } from './Greeting';

test('renders the name', () => {
  render(<Greeting name="Ada" />);
  expect(screen.getByText(/Ada/)).toBeInTheDocument();
});
```
Run: `npm test` (or `npm test -- --coverage`). Co-locate `*.test.tsx` beside the component.

## What to test
- Backend: endpoint contracts/status codes, service logic, validation/error paths.
- Frontend: rendering, user interactions, hook behavior; mock the `src/api/` client.
- Add/adjust tests in the same PR as the change; keep them green before merge ([[pr-review]]).

## Commands at a glance
```bash
dotnet test backend/Mtsorella.slnx                                       # backend (unit + integration; needs Docker)
dotnet test backend/tests/Mtsorella.Api.Tests/Mtsorella.Api.Tests.csproj # backend, fast unit suite only
cd frontend && npm test                                                  # frontend
```
