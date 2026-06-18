---
name: pr-review
description: Review a diff or pull request for the mtsorella repo against its conventions before merge — backend, frontend, skills, commit hygiene, and CI. Use when asked to review a PR or local changes.
---

# pr-review

Review a diff/PR for **xforman2/mtsorella** against the repo's conventions before merge. Use
when the user asks to review a PR, a branch, or working-tree changes.

## Get the diff
```bash
gh pr diff <N> --repo xforman2/mtsorella          # a specific PR
gh pr view <N> --repo xforman2/mtsorella --json title,body,files
git diff main...HEAD                              # local branch vs main
```

## Review order
1. Read the PR description and the linked issue(s) — does the change match intent and scope?
2. Skim the file list for anything unexpected (secrets, generated files, unrelated churn).
3. Walk the diff hunk by hunk against the checklists below.
4. Summarize: blocking issues first, then nits. Be specific (file:line).

## Checklists

### Backend (`/backend`) — see [[dotnet-backend]]
- [ ] DTOs (`record`) used at the boundary; EF entities not exposed directly.
- [ ] `async`/`await` end to end; `CancellationToken` threaded through.
- [ ] Errors return `ProblemDetails`; no leaked exception/stack details.
- [ ] Endpoints have `.WithName`/`.WithTags` (clean OpenAPI/Swagger docs).
- [ ] Tests added/updated (see [[test-suite]]); nullable warnings clean.

### Frontend (`/frontend`) — see [[react-frontend]]
- [ ] Function components with typed props; Rules of Hooks respected.
- [ ] API calls go through `src/api/` (`apiFetch`); types live in `src/api/types.ts` (hand-written, agreed with backend).
- [ ] No hardcoded API URLs/secrets; config via `import.meta.env.VITE_*`.
- [ ] No stray `any`; `strict` satisfied; styles use CSS Modules.

### Skills (`.claude/skills/`)
- [ ] `SKILL.md` has valid frontmatter (`name`, `description` with a "Use when…" trigger).
- [ ] Style matches existing skills; cross-links use `[[slug]]`.

### Cross-cutting
- [ ] Commits follow [[conventional-commits]].
- [ ] **No AI attribution** — no "Generated with Claude Code" footer, no `Co-Authored-By` trailer
      (see [[gh-issues]]).
- [ ] CI is green (see [[gh-actions-ci]]); docs updated where needed (see [[docs-keeper]]).
- [ ] PR links its issue(s) with `Closes #N`.

## Output
Post review as a concise comment (or inline). Group as **Blocking** / **Suggestions** / **Nits**.
Approve only when blocking items are resolved and CI passes.
