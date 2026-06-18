---
name: react-frontend
description: Conventions for the mtsorella React frontend (Vite + TypeScript) — folder layout, function-component and hook patterns, typed data fetching, env config, and styling. Use when adding or changing frontend code under /frontend.
---

# react-frontend

Conventions for the **Vite + React + TypeScript** app under `/frontend`. Use this when building
components, hooks, or data-fetching code.

> The frontend is scaffolded per issue #4. Until then, treat the layout below as the target.

## Project layout
```
frontend/
  src/
    main.tsx                 # entry
    App.tsx                  # root component / routes
    components/              # reusable presentational components
    features/<feature>/      # feature-scoped components + hooks
    hooks/                   # shared hooks (useXxx)
    api/                     # thin typed fetch client + hand-written types (no codegen)
    lib/                     # helpers, formatting, config
    styles/                  # global styles
  index.html
  vite.config.ts
```

## Components
- **Function components only**, typed props via an explicit `type Props = { ... }`.
- One component per file; `PascalCase.tsx` for components, `camelCase.ts` for utilities.
- Co-locate a component's styles (`Foo.module.css`) and tests (`Foo.test.tsx`) next to it.
- Keep components presentational; push data/effect logic into hooks.

```tsx
type GreetingProps = { name: string };

export function Greeting({ name }: GreetingProps) {
  return <p className={styles.greeting}>Hello, {name}</p>;
}
```

## Hooks
- Custom hooks are `useX` and obey the Rules of Hooks (top level only).
- Encapsulate data fetching and side effects in hooks (e.g. `useHealth()`), not components.

## Data fetching
- Use the thin typed `fetch` client in `src/api/` (`apiFetch<T>`). Request/response **types are
  hand-written** in `src/api/types.ts` and agreed directly with the backend developer (#3) — no
  OpenAPI/codegen.
- **TanStack Query** is optional and recommended once caching/refetching needs appear; if added,
  keep query keys in `src/api/`.
- Read the API base URL from env (`src/lib/config.ts`), never hardcode.

## Environment / config
- Vite env vars are prefixed `VITE_` and read via `import.meta.env.VITE_*`.
- Example: `const baseUrl = import.meta.env.VITE_API_URL ?? "/api";`
- Document new vars in the frontend README (see [[docs-keeper]]).

## Styling
- Default to **CSS Modules** (`*.module.css`) for component-scoped styles.
- Keep global resets/tokens in `src/styles/`.

## Quality
- TypeScript `strict` stays on; no `any` without justification.
- Run lint/format before committing; tests via [[test-suite]] (`npm test`).
- Follow [[conventional-commits]]; no AI attribution (see [[gh-issues]]).
