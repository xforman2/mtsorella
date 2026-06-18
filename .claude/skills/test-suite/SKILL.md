---
name: test-suite
description: Generate and run tests for the mtsorella monorepo — xUnit for the .NET backend and Vitest + React Testing Library for the React frontend. Use when adding tests or running the suites.
---

# test-suite

Conventions and commands for testing both stacks. Use when writing tests or running them locally
or in CI (see [[gh-actions-ci]]).

## Backend — xUnit (see [[dotnet-backend]])
Layout: `backend/tests/Mtsorella.Api.Tests/`, mirroring `src/`.

- **Unit tests** for services/helpers — plain xUnit, no host.
- **Integration tests** boot the API in-memory with `WebApplicationFactory<Program>`
  (requires `public partial class Program;` in `Program.cs`).

```csharp
public class HealthEndpointTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Health_returns_ok()
    {
        var client = factory.CreateClient();
        var res = await client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, res.StatusCode);
    }
}
```
Package: `Microsoft.AspNetCore.Mvc.Testing`. Run: `dotnet test backend/Mtsorella.slnx`.

## Frontend — Vitest + React Testing Library (see [[react-frontend]])
Setup (once, in `/frontend`):
```bash
npm i -D vitest @testing-library/react @testing-library/jest-dom @testing-library/user-event jsdom
```
`vite.config.ts`: `test: { environment: 'jsdom', setupFiles: './src/test/setup.ts', globals: true }`
and add `"test": "vitest"` to `package.json` scripts.

```tsx
import { render, screen } from '@testing-library/react';
import { Greeting } from './Greeting';

test('renders the name', () => {
  render(<Greeting name="Ada" />);
  expect(screen.getByText(/Ada/)).toBeInTheDocument();
});
```
Run: `npm test` (or `npm test -- --coverage`). Co-locate `*.test.tsx` beside the component.

## What to test
- Backend: endpoint contracts/status codes, service logic, validation/error paths.
- Frontend: rendering, user interactions, hook behavior; mock the `src/api/` client.
- Add/adjust tests in the same PR as the change; keep them green before merge ([[pr-review]]).

## Commands at a glance
```bash
dotnet test backend/Mtsorella.slnx        # backend
cd frontend && npm test                   # frontend
```
