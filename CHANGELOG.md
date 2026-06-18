# Changelog

All notable changes to this project are documented here, following
[Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [Unreleased]

### Added

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
