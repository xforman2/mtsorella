---
name: api-contract-sync
description: Keep the mtsorella frontend's TypeScript API client in sync with the backend OpenAPI spec so FE/BE types never drift. Use when backend endpoints/DTOs change or the frontend needs typed API access.
---

# api-contract-sync

Generate the frontend's typed API client from the backend's **OpenAPI** spec so request/response
types stay in lockstep. Use when backend contracts change or the frontend needs to call the API.

## Why
The backend ([[dotnet-backend]]) is the single source of truth. Hand-written frontend types drift.
This skill regenerates `frontend/src/api/` from the spec, so a contract change surfaces as a TS
compile error rather than a runtime bug.

## Tooling
- **openapi-typescript** — generates TS types from the OpenAPI document.
- **openapi-fetch** — a tiny, fully-typed fetch client that consumes those types.

```bash
cd frontend
npm i -D openapi-typescript
npm i openapi-fetch
```

## Workflow
1. Produce the spec from the backend (`/openapi/v1.json`):
   ```bash
   # Option A: run the API (dev) and curl it
   curl -s http://localhost:5xxx/openapi/v1.json -o frontend/src/api/openapi.json
   # Option B: generate at build time with the Microsoft.Extensions.ApiDescription.Server tooling
   ```
2. Generate types:
   ```bash
   npx openapi-typescript frontend/src/api/openapi.json -o frontend/src/api/schema.d.ts
   ```
3. Create the client once (`frontend/src/api/client.ts`):
   ```ts
   import createClient from "openapi-fetch";
   import type { paths } from "./schema";
   export const api = createClient<paths>({ baseUrl: import.meta.env.VITE_API_URL ?? "/api" });
   ```
4. Use it from hooks/components ([[react-frontend]]):
   ```ts
   const { data, error } = await api.GET("/health");
   ```
5. Commit `schema.d.ts` (and `openapi.json`) so reviewers see contract changes in the diff.

## Keep it from drifting
- Add an npm script: `"gen:api": "openapi-typescript src/api/openapi.json -o src/api/schema.d.ts"`.
- **CI drift check** (wire into [[gh-actions-ci]]): regenerate and fail if `git diff --exit-code`
  shows the committed `schema.d.ts` is stale.
- Regenerate whenever a backend endpoint or DTO changes; flag it in [[pr-review]].
- Commit as `chore(frontend): regen api client` per [[conventional-commits]].
