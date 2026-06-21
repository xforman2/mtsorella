---
name: gh-actions-ci
description: Author and maintain GitHub Actions workflows for the mtsorella monorepo — path-filtered backend (.NET) and frontend (Node) build/test jobs with caching. Use when adding or changing CI under .github/workflows.
---

# gh-actions-ci

Conventions for CI workflows under `.github/workflows/`. Use when creating or editing GitHub
Actions for this monorepo. Tracks issue #14.

## Principles
- **Path-filter** so each job runs only when its part of the monorepo changes.
- Use official setup actions with built-in dependency **caching**.
- Trigger on `pull_request` and pushes to `main`. Keep jobs fast and independent.

## Reference workflow (`.github/workflows/ci.yml`)
```yaml
name: CI
on:
  push: { branches: [main] }
  pull_request:

jobs:
  backend:
    runs-on: ubuntu-latest
    if: ${{ github.event_name == 'push' }} # PRs use the path filter below instead
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-dotnet@v4
        with:
          dotnet-version: '10.x'
          cache: true
          cache-dependency-path: backend/**/packages.lock.json
      - run: dotnet restore backend/Mtsorella.slnx
      - run: dotnet build backend/Mtsorella.slnx --no-restore
      - run: dotnet test backend/Mtsorella.slnx --no-build

  frontend:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
      - uses: actions/setup-node@v4
        with:
          node-version: '22'
          cache: npm
          cache-dependency-path: frontend/package-lock.json
      - run: npm ci
        working-directory: frontend
      - run: npm run build
        working-directory: frontend
      - run: npm test
        working-directory: frontend
```

## Path filtering
Prefer per-path triggers using `paths:` on the workflow, or split into `ci-backend.yml`
(`paths: ['backend/**']`) and `ci-frontend.yml` (`paths: ['frontend/**']`) so unrelated changes
don't run both. For required-check stability on a monorepo, a `paths`-based `dorny/paths-filter`
gate job is a good middle ground.

Backend CI is implemented this way: **`.github/workflows/ci-backend.yml`**, path-filtered to
`backend/**` (+ the workflow file itself), running build + the full test suite + coverage on `ubuntu-latest`.
Frontend CI (`ci-frontend.yml`) is still to come.

## Integration tests (Docker / Testcontainers)
The integration suite uses `Testcontainers.PostgreSql` (see [[test-suite]]). `ubuntu-latest` ships a Docker
daemon, so `dotnet test backend/Mtsorella.slnx` runs unit **and** integration tests natively — no `services:`
block (Testcontainers manages its own container). If Docker Hub anonymous-pull rate limits ever flake the
job, add a `docker/login-action@v3` step with a `DOCKERHUB_TOKEN` secret before the test step.

## Coverage in CI
Report coverage in the run itself — no external service needed:
- Collect with coverlet during the test step: `dotnet test ... --collect:"XPlat Code Coverage" --results-directory backend/TestResults` (every test project references `coverlet.collector`).
- Turn the Cobertura XML into a GitHub-flavored summary with the pinned `dotnet-reportgenerator-globaltool`
  (`backend/.config/dotnet-tools.json`; run `dotnet tool restore` from `backend/`):
  `reportgenerator -reports:"TestResults/**/coverage.cobertura.xml" -targetdir:TestResults/coverage-report -reporttypes:"MarkdownSummaryGithub;Html" -filefilters:"-**/Migrations/*" -classfilters:"-Mediator.*;-Microsoft.*;-System.*"`.
- Append `SummaryGithub.md` to `$GITHUB_STEP_SUMMARY` (renders on the run page) and upload the HTML folder
  via `actions/upload-artifact@v4`. Guard these steps with `if: ${{ !cancelled() }}` so coverage still shows
  when a test fails. The `-filefilters`/`-classfilters` drop migrations and generated Mediator/OpenAPI/framework
  code so the headline % reflects our own code (the same filters [[test-suite]] uses locally).
- Report-only for now (no threshold/gate); add a soft floor once real feature slices exist.

## Conventions
- Pin actions to a major version (`@v4`). Keep secrets in repo/Org secrets, never inline.
- Build + test commands must match [[test-suite]]; a green run is required before merge ([[pr-review]]).
- Commit workflow changes as `ci: ...` per [[conventional-commits]].
