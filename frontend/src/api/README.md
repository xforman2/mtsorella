# API client

`client.ts` is a thin typed `fetch` wrapper (`apiFetch<T>`) over the backend API. It uses the
base URL from `../lib/config` (`VITE_API_URL`, see `.env.example`).

**No OpenAPI / codegen.** Request and response types live in `types.ts` and are written by hand,
coordinated directly with the backend developer (issue #3). Add new shapes to `types.ts` as
endpoints are agreed.
