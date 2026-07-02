# MT Sorella

Monorepo for the **MT Sorella** competitive majorette team website and app — a public site, a
members' gamification zone, and an admin panel for coaches.

## Layout

```
mtsorella/
  frontend/        React + Vite + TypeScript app  (this is scaffolded)
  backend/         ASP.NET Core (.NET 10) Web API  (domain, persistence, auth)
  prototype/       Self-contained hi-fi prototype (design reference)
  requirements.md  Functional requirements (FR / FE / BE)
  .claude/skills/  Claude Code skills (conventions for this repo)
```

## Prerequisites

- Node.js ≥ 20 (developed on Node 26) + npm — for the frontend
- .NET 10 SDK — for the backend
- Docker — only for the backend's Testcontainers integration tests

## Frontend

```bash
cd frontend
npm install
cp .env.example .env
npm run dev          # http://localhost:5173
```

See [`frontend/README.md`](frontend/README.md) for all scripts, env vars, and structure.

## Backend

```bash
cd backend
dotnet tool restore
dotnet ef database update --project src/Mtsorella.Api
dotnet run --project src/Mtsorella.Api   # /scalar, /openapi/v1.json, /health
```

ASP.NET Core (.NET 10) Web API — Vertical Slice Architecture over PostgreSQL, with the domain +
persistence layers, the transactional outbox, and email/password auth (JWT). See
[`backend/README.md`](backend/README.md) for setup, configuration (including required secrets), and tests.

## Conventions

Repo conventions are encoded as Claude Code skills under `.claude/skills/` (issue creation,
React/.NET patterns, testing, PR review, conventional commits, docs). Commits follow
Conventional Commits; the changelog lives in [`CHANGELOG.md`](CHANGELOG.md).
