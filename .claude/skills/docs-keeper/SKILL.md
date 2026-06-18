---
name: docs-keeper
description: Keep the mtsorella docs in sync with the code — root and per-package READMEs, the changelog, and API docs derived from OpenAPI. Use after changes that affect setup, endpoints, env vars, scripts, or architecture.
---

# docs-keeper

Keep documentation accurate as the code changes. Use this after any change that affects how the
project is set up, run, or consumed.

## Docs in scope
- **Root `README.md`** — what the repo is, monorepo layout, how to run backend + frontend, prereqs.
- **`backend/README.md`** — backend setup, run/test commands, env/config, migrations.
- **`frontend/README.md`** — frontend setup, scripts, `VITE_*` env vars, where the API client and
  hand-written types live (`src/api/`).
- **`CHANGELOG.md`** — maintained per [[conventional-commits]] (Keep a Changelog style).
- **API docs** — the backend's OpenAPI/Swagger UI. The frontend's `src/api/types.ts` is
  hand-written and agreed with the backend developer (no codegen).

## Update triggers (update docs in the *same* PR)
| Change | Update |
|--------|--------|
| New/changed run or build command | relevant README "Getting started" |
| New env var / config | README env section (+ `.env.example` if present) |
| New/changed endpoint or DTO | update frontend `src/api/types.ts` (agreed with backend); note in changelog |
| New script / tool | README scripts section |
| Architecture / layout change | root README layout section |
| Any user-facing change | `CHANGELOG.md` `[Unreleased]` |

## How to work
1. Diff the change and ask: does any doc now describe something untrue?
2. Update the smallest set of docs that restores accuracy — keep it concise, link don't duplicate.
3. Verify commands you document actually run.
4. Commit as `docs(<scope>): ...` per [[conventional-commits]]; checked during [[pr-review]].

## Conventions
- Prefer one canonical place for a fact; cross-link instead of copying.
- No AI attribution anywhere (see [[gh-issues]]).
