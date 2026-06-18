# MT Sorella — Frontend

React + Vite + TypeScript app for the MT Sorella majorette team. Part of the
[mtsorella monorepo](../README.md).

## Prerequisites

- Node.js ≥ 20 (developed on Node 26)
- npm

## Getting started

```bash
cd frontend
npm install
cp .env.example .env   # adjust VITE_API_URL if needed
npm run dev            # http://localhost:5173
```

## Scripts

| Script            | What it does                                      |
| ----------------- | ------------------------------------------------- |
| `npm run dev`     | Start the Vite dev server (HMR)                   |
| `npm run build`   | Type-check (`tsc -b`) + production build          |
| `npm run preview` | Preview the production build                      |
| `npm run lint`    | ESLint                                            |
| `npm run format`  | Prettier write · `npm run format:check` to verify |
| `npm test`        | Vitest (watch) · `npm run test:run` for CI        |

## Environment

Vite env vars are prefixed `VITE_` and read via `import.meta.env`. See `.env.example`.

- `VITE_API_URL` — backend base URL (consumed in `src/lib/config.ts`; defaults to `/api`).

## Structure

```
src/
  main.tsx          entry
  App.tsx           router (public / member / admin zones)
  components/       shared presentational components (+ Layout shells)
  features/         feature-scoped pages (public / member / admin)
  hooks/            shared hooks (useXxx)
  api/              typed fetch client + hand-written types (no codegen)
  lib/              helpers, config
  styles/           design tokens + global styles (CSS Modules elsewhere)
  test/             Vitest setup
```

Conventions live in the repo's Claude Code skills (`.claude/skills/react-frontend`,
`…/test-suite`). Notably: function components with typed props, **CSS Modules** for styling,
and API types **hand-written and coordinated with the backend developer** (no OpenAPI codegen).
