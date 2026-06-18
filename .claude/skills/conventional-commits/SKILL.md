---
name: conventional-commits
description: Write Conventional Commits messages and maintain the changelog for the mtsorella repo. Use when committing changes or preparing a release/changelog entry.
---

# conventional-commits

Write commit messages in the **Conventional Commits** format and keep the changelog current.

## Format
```
<type>(<scope>): <subject>

<body — what & why, wrapped ~72 cols>

<footer — e.g. "Closes #12", "BREAKING CHANGE: ...">
```
- **subject**: imperative mood, lowercase, no trailing period (e.g. "add health endpoint").
- **scope** (optional): the area touched — `backend`, `frontend`, `ci`, `skills`, `repo`.

## Types (map to repo labels — see [[gh-issues]])
| type | use for | label |
|------|---------|-------|
| `feat` | a new feature | `type:feature` |
| `fix` | a bug fix | `type:bug` |
| `docs` | docs only | `type:docs` |
| `chore` | tooling/maintenance | `type:chore` |
| `refactor` | non-behavioral code change | `type:chore` |
| `test` | tests only | — |
| `ci` | CI/CD changes | `area:ci` |
| `build` | build/deps | `type:chore` |

## Examples
```
feat(backend): add /health endpoint
fix(frontend): guard against undefined api response
docs(repo): document monorepo layout in README
ci: add path-filtered build jobs for backend and frontend
```

## No AI attribution (repo rule)
Never add a `Co-Authored-By: Claude ...` trailer or a "Generated with Claude Code" line to commit
messages. (Same rule for issues/PRs — see [[gh-issues]] and [[pr-review]].)

## Changelog
- Keep a `CHANGELOG.md` at the repo root in **Keep a Changelog** style with an `## [Unreleased]`
  section grouped by `Added` / `Changed` / `Fixed` / `Removed`.
- Add an entry in the same PR as the change; move `Unreleased` items under a versioned heading
  (e.g. `## [0.1.0] - YYYY-MM-DD`) at release time.
- Derive entries from commit subjects since the last release:
  `git log <lastTag>..HEAD --pretty='- %s'`.
- Keeping the changelog in sync is part of [[docs-keeper]].
