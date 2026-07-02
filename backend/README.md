# MT Sorella — Backend

ASP.NET Core (.NET 10) Web API, organized as **Vertical Slice Architecture** over a single PostgreSQL
database. Conventions live in the `dotnet-backend` skill (`.claude/skills/dotnet-backend/`).

## Prerequisites

- .NET 10 SDK
- PostgreSQL (local instance, or use the connection string of your choice)
- Docker — only for the Testcontainers-backed integration tests

## Getting started

```bash
cd backend
dotnet tool restore                                   # pinned dotnet-ef
dotnet ef database update --project src/Mtsorella.Api # apply migrations
dotnet run --project src/Mtsorella.Api                # /scalar, /openapi/v1.json, /health
```

Interactive API docs are at `/scalar`; the OpenAPI document at `/openapi/v1.json`.

## Configuration

Config is read from `appsettings.json` + `appsettings.Development.json`, then environment variables /
user-secrets (which override files). Secrets must **not** be committed — use `dotnet user-secrets` in
development and environment variables in production.

| Key | Purpose | Where to set |
| --- | --- | --- |
| `ConnectionStrings:Default` | PostgreSQL connection | appsettings / `ConnectionStrings__Default` |
| `Jwt:Issuer`, `Jwt:Audience` | Token issuer/audience | appsettings (defaults `mtsorella`) |
| `Jwt:AccessTokenMinutes` | Access-token lifetime (default 60) | appsettings |
| `Jwt:Secret` | HMAC-SHA256 signing key (≥ 32 chars) | **secret** — `Jwt__Secret` / user-secrets |
| `Admin:Email`, `Admin:Password` | First admin seeded on startup if no admin exists | **secret** — env / user-secrets |

**`Jwt:Secret`** — when unset, the app boots with a *random per-process* key so dev/test/CI work without
configuration. Such tokens do not survive a restart and won't validate across instances, so **production
must set a stable `Jwt__Secret`**. The admin seed is idempotent and a no-op when `Admin:*` is unset.

```bash
# Development secrets (not committed)
cd src/Mtsorella.Api
dotnet user-secrets init
dotnet user-secrets set "Jwt:Secret" "a-long-random-development-signing-key-change-me"
dotnet user-secrets set "Admin:Email" "coach@mtsorella.sk"
dotnet user-secrets set "Admin:Password" "ChangeMeOnFirstLogin1"
```

## Authentication

Login by email + password; there is no public registration — accounts are created by an admin. Roles:
guest (no token) / member / admin (coach). See the `dotnet-backend` skill's *Auth & authorization*
section for how slices are gated. Flow:

1. A coach signs in (`POST /auth/login`) and reviews prihlášky (`GET /admin/applications`).
2. `POST /admin/accounts` (admin) turns an application into a Member + login account, returning a
   one-time temporary password to hand over.
3. The member signs in with it (`mustChangePassword: true`) and calls `POST /auth/change-password`.

## Migrations

```bash
cd backend
dotnet ef migrations add <Name> --project src/Mtsorella.Api --output-dir Persistence/Migrations
dotnet ef database update --project src/Mtsorella.Api
```

## Tests

```bash
dotnet test tests/Mtsorella.Api.Tests/Mtsorella.Api.Tests.csproj   # fast unit suite, no Docker
dotnet test Mtsorella.slnx                                         # + Testcontainers integration (needs Docker)
```
