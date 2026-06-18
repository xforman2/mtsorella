---
name: gh-issues
description: Create polished, consistently-labeled GitHub issues for the mtsorella repo and add them to the roadmap Projects board. Use when the user asks to file, create, open, or track an issue/task in this repo.
---

# gh-issues

Create clean, well-labeled GitHub issues for this repo that match its label taxonomy,
milestones, and issue-template structure — and add them to the roadmap Projects board. This is
the repo's bootstrap skill: file every task through it so tracking stays consistent.

> **Repo-agnostic:** commands below resolve the repo/owner from the local clone, so this skill
> works for **any contributor** and survives forks/renames/transfers — no hardcoded slug.

## Prerequisites (any contributor, not just the repo owner)
- `gh` installed and authenticated: `gh auth login` (or `gh auth status` to verify).
- **Write or triage access** to the repo (to create issues / apply labels).
- For the Projects board only: the `project` scope — once per machine:
  `gh auth refresh -s project --hostname github.com`.
- Run commands **from inside a clone** of the repo (so auto-detection works).

## Resolve repo + owner (run this first in any issue-creating session)
```bash
REPO="$(gh repo view --json nameWithOwner --jq .nameWithOwner)"   # e.g. xforman2/mtsorella
OWNER="${REPO%%/*}"                                               # e.g. xforman2
```
If you are not inside a clone, set them explicitly: `REPO=xforman2/mtsorella; OWNER=xforman2`.
(For a fork, `gh repo view` targets the fork; pass `--repo <upstream>` to target upstream.)

## Label taxonomy (always pick exactly one `type:` + one `area:`; add `priority:`/`status:` as known)

| Group | Labels |
|-------|--------|
| **type:** | `type:infra` · `type:skill` · `type:feature` · `type:bug` · `type:docs` · `type:chore` |
| **area:** | `area:backend` · `area:frontend` · `area:skills` · `area:ci` · `area:repo` |
| **priority:** | `priority:high` · `priority:medium` · `priority:low` |
| **status:** | `status:ready` · `status:in-progress` · `status:blocked` |

Rules of thumb:
- Always apply one `type:` and one `area:`. Add `priority:` for anything non-trivial.
- Use `status:in-progress` only when work has actually started; otherwise omit.
- Labels must already exist server-side. (Re)create idempotently:
  `gh label create "<name>" --color <hex> --description "<d>" --repo "$REPO" --force`.

## Milestones
- **`M0 — Repo bootstrap`** — scaffolding, root config, labels/templates/board.
- **`M1 — Skills foundation`** — Claude Code skills + CI.

Verify/list: `gh api "repos/$REPO/milestones" --jq '.[].title'`.

## Title conventions (match the issue templates)
- Skill: `skill: <slug> — <summary>`
- Infra: `infra: <summary>`
- Bug: `bug: <summary>`
- Chore: `chore: <summary>`
- Feature: `feat: <summary>`

## Body structure (keep issues "pretty" and scannable)
```markdown
## Goal / Purpose
<one or two sentences>

## Acceptance criteria
- [ ] ...
- [ ] ...

## Notes
<context, links, dependencies>
```

## Repo convention: no AI attribution
Never add AI/tool attribution to anything in this repo. Specifically:
- No `🤖 Generated with Claude Code` (or similar) footer in issue bodies, PR bodies, or comments.
- No `Co-Authored-By: Claude ...` trailer in commit messages.

Keep issues, PRs, and commits attribution-free.

## Workflow
1. Resolve `REPO`/`OWNER` (above). Decide `type` + `area` + `priority`, milestone, and title.
2. Create the issue (pass `--label` once per label):

```bash
gh issue create --repo "$REPO" \
  --title "infra: scaffold .NET 10 backend Web API" \
  --label "type:infra" --label "area:backend" --label "priority:high" \
  --milestone "M0 — Repo bootstrap" \
  --body "$(cat <<'EOF'
## Goal
Scaffold the ASP.NET Core Web API project on .NET 10 under /backend.

## Acceptance criteria
- [ ] Solution + Mtsorella.Api project build
- [ ] /health endpoint returns 200
- [ ] xUnit test project wired up

## Notes
Done collaboratively with the maintainer.
EOF
)"
```

3. Capture the printed issue URL/number for follow-up (e.g. adding to the board).

## Add to the roadmap Projects board
Requires the `project` scope (see Prerequisites). The board is owned by `$OWNER`.

```bash
# Find the board number:
gh project list --owner "$OWNER"
# Add an issue to it:
gh project item-add <PROJECT_NUMBER> --owner "$OWNER" \
  --url "https://github.com/$REPO/issues/<N>"
```
If the board lives under a different account/org than the repo owner, set `OWNER` to that account.

## Tips
- Verify your work: `gh issue list --repo "$REPO" --label "type:skill"`.
- Keep one issue = one deliverable. Split large asks into several linked issues.
- A contributor without the `project` scope can still create issues — only the board step needs it.
