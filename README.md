# MT Sorella

Monorepo for the **MT Sorella** competitive majorette team website and app — a public site, a
members' gamification zone, and an admin panel for coaches.

## Layout

```
mtsorella/
  frontend/        React + Vite + TypeScript app  (this is scaffolded)
  backend/         ASP.NET Core (.NET 10) Web API  (planned — issue #3)
  prototype/       Self-contained hi-fi prototype (design reference)
  requirements.md  Functional requirements (FR / FE / BE)
  .claude/skills/  Claude Code skills (conventions for this repo)
```

## Prerequisites

- Node.js ≥ 20 (developed on Node 26) + npm — for the frontend
- .NET 10 SDK — for the backend (once scaffolded)

## Frontend

```bash
cd frontend
npm install
cp .env.example .env
npm run dev          # http://localhost:5173
```

See [`frontend/README.md`](frontend/README.md) for all scripts, env vars, and structure.

## Backend

Not yet scaffolded — tracked in issue #3 (owned by @mf-16). It will live under `backend/`
(ASP.NET Core Web API on .NET 10).

## Conventions

Repo conventions are encoded as Claude Code skills under `.claude/skills/` (issue creation,
React/.NET patterns, testing, PR review, conventional commits, docs). Commits follow
Conventional Commits; the changelog lives in [`CHANGELOG.md`](CHANGELOG.md).
