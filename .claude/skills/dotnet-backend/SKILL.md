---
name: dotnet-backend
description: Conventions for the mtsorella .NET backend (ASP.NET Core, .NET 10) — Vertical Slice Architecture, how to write clean C# (no class primary constructors, SOLID/DRY), and the slice/Mediator/FluentValidation/ErrorOr/EF Core patterns. Use when adding or changing backend code under /backend.
---

# dotnet-backend

How to write and structure backend code under `backend/`. The API is an ASP.NET Core (.NET 10)
Web API organized as **Vertical Slice Architecture (VSA)** with a single PostgreSQL database.
Tests live in [[test-suite]]; CI in [[gh-actions-ci]]; the frontend consumes the OpenAPI doc via
[[api-contract-sync]]; commit per [[conventional-commits]]; review per [[pr-review]].

## Architecture: vertical slices, one database
- Organize by **feature**, not by technical layer. Each slice owns its request, validation, business
  logic, and route — all together. There is **no** `Controllers/` or `Services/` folder.
- **One** shared database and **one** `AppDbContext`. "Vertical slice" is about code organization,
  not splitting data stores (that would be microservices).
- Shared things stay shared: domain entities (`Domain/`), the `DbContext` + repositories
  (`Persistence/`), and cross-cutting plumbing (`Common/`). Everything feature-specific goes in the
  slice; data access goes through a repository (see Data access).
- A slice may depend on another feature's behavior by sending its request through `ISender`
  (`await sender.Send(new OtherFeature.Command(...))`) — never by reaching into another feature's
  tables directly.

## Repo structure
```
backend/
├── Mtsorella.slnx                      # solution (slnx format)
├── .config/dotnet-tools.json           # pinned local CLI tools (dotnet-ef); run `dotnet tool restore`
├── src/Mtsorella.Api/
│   ├── Program.cs                       # composition root; ends with `public partial class Program;`
│   ├── Common/                          # cross-cutting plumbing shared by all slices
│   │   ├── Endpoints/                   # IEndpoint + auto-mapping
│   │   ├── Behaviors/                   # Mediator pipeline behaviors (e.g. validation)
│   │   └── Results/                     # ErrorOr -> HTTP mapping
│   ├── Domain/                          # shared entities (map to the one DB)
│   ├── Persistence/                     # AppDbContext, Migrations/
│   │   └── Repositories/                # IRepository<T>/Repository<T> + per-aggregate repos
│   └── Features/<Area>/<Feature>.cs     # the slices
└── tests/Mtsorella.Api.Tests/           # xUnit, mirrors src/ ([[test-suite]])
```
- One file per slice while small; split a slice into its own folder once the file gets long
  (~150–200 lines) — never split it across the old technical-layer folders.

## Clean-code conventions (house style)

### Constructors — NO primary constructors on classes
Use an **explicit constructor with `readonly` fields** for classes (services, handlers, `DbContext`,
test fixtures). Do **not** use C# 12 class primary constructors.

```csharp
// ✗ Do NOT do this on a class
public sealed class Handler(AppDbContext dbContext) : IRequestHandler<Command, ErrorOr<Guid>> { ... }

// ✓ Do this
public sealed class Handler : IRequestHandler<Command, ErrorOr<Guid>>
{
    private readonly AppDbContext _dbContext;

    public Handler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    // ...
}
```
**Records are the exception:** positional `record` declarations are encouraged for immutable
commands/queries/DTOs — that is a different feature and the idiomatic way to model data:
```csharp
public sealed record Command(string Name, decimal Price) : IRequest<ErrorOr<Guid>>;
public sealed record Response(Guid Id, string Name, decimal Price);
```

### Naming & general style
- `PascalCase` for types, methods, properties, public members; `_camelCase` for private fields;
  `camelCase` for locals and parameters.
- File-scoped namespaces (`namespace X;`). One top-level type per file (nested slice types such as
  `Command`/`Validator`/`Handler` inside the slice class are fine and intended).
- `sealed` by default on classes that are not designed for inheritance (slices, handlers, behaviors).
- Nullable reference types are **on**; use `required` for non-null members instead of `= null!`.
- **Async all the way:** suffix async methods with `Async` (except Mediator `Handle`, which the
  interface fixes), return `Task`/`ValueTask`, and **always accept and pass `CancellationToken`**.
- Prefer expression-bodied members for trivial one-liners; explicit blocks otherwise.
- No unused `using`s; keep `using`s sorted (System first).

### Comments & documentation — explain the non-obvious only
Do **not** add XML doc comments or comments that merely restate what the code already says.
A `Product` POCO, a standard `DbContext`, a `GetHealth` endpoint, and a handler that saves an entity
are self-explanatory — leave them undocumented.

**Do** document the non-obvious: shared plumbing whose purpose/mechanics aren't visible at the call
site, deliberate trade-offs, and "magic." In this codebase that means `IEndpoint`,
`EndpointExtensions` (reflection-based auto-registration), `ValidationBehavior` (auto-running
pipeline), `ErrorOrResults` (error→HTTP mapping), and inline notes for genuinely surprising lines
(the `(dynamic)` cast, the `Http` namespace alias). Rule of thumb: if a comment would just narrate
the next line, delete it; if it explains *why* or *what this is for* in a way the code can't, keep it.
Architecture/teaching belongs in this skill, not in per-file docstrings.

## Anatomy of a slice
Everything for one feature lives in one file. Endpoint registration is via `IEndpoint` (auto-mapped
at startup by `Common/Endpoints/EndpointExtensions`), so `Program.cs` never lists routes.

```csharp
public sealed class CreateProduct : IEndpoint
{
    // 1. Request (immutable record)
    public sealed record Command(string Name, decimal Price, int StockQuantity)
        : IRequest<ErrorOr<Guid>>;

    // 2. Validation (FluentValidation) — runs automatically via the pipeline behavior
    public sealed class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty().MaximumLength(200);
            RuleFor(c => c.Price).GreaterThan(0);
        }
    }

    // 3. Handler — pure business logic; explicit ctor + readonly field; data access via a repository
    public sealed class Handler : IRequestHandler<Command, ErrorOr<Guid>>
    {
        private readonly IProductRepository _products;
        public Handler(IProductRepository products) => _products = products;

        public async ValueTask<ErrorOr<Guid>> Handle(Command command, CancellationToken ct)
        {
            var product = new Product { Id = Guid.NewGuid(), Name = command.Name, /* ... */ };
            await _products.AddAsync(product, ct);
            await _products.SaveChangesAsync(ct);
            return product.Id;
        }
    }

    // 4. Route
    public void MapEndpoint(IEndpointRouteBuilder app) =>
        app.MapPost("/products", async (Command command, ISender sender) =>
            {
                ErrorOr<Guid> result = await sender.Send(command);
                return result.ToHttpResult(id => Results.Created($"/products/{id}", new { id }));
            })
            .WithName("CreateProduct").WithTags("Products");
}
```

## Mediator (source-generated, free)
- Package: `Mediator` (martinothamar) — **not** MediatR. Configured `ServiceLifetime.Scoped` so
  handlers can use scoped dependencies (repositories, `DbContext`).
- Requests implement `IRequest<TResponse>`; handlers implement `IRequestHandler<TRequest, TResponse>`
  with `ValueTask<TResponse> Handle(TRequest, CancellationToken)`.
- Endpoints depend on `ISender` and call `Send`. Keep endpoints thin — no business logic in the
  route lambda beyond `Send` + `ToHttpResult`.

## Validation — behavior, not in handlers
Write a `Validator : AbstractValidator<TRequest>` in the slice and **nothing else**. The
`ValidationBehavior` pipeline runs all registered validators before the handler and short-circuits
with the failures as an `ErrorOr` value. **Never** validate inputs inside a handler. A request with
no validator simply skips validation.

## Errors — ErrorOr, not exceptions for control flow
- Handlers return `ErrorOr<T>`. Express expected failures as values: `Error.NotFound(...)`,
  `Error.Conflict(...)`, `Error.Validation(...)`.
- Endpoints map the result with `result.ToHttpResult(onSuccess)` (`Common/Results/ErrorOrResults`),
  which turns error types into the right HTTP status (400/404/409/...). Do not throw to signal
  business outcomes; reserve exceptions for truly exceptional/unexpected faults.

## Data access — repositories
- Handlers depend on a **repository abstraction**, never on `AppDbContext` directly. This keeps EF out
  of the slices and lets handlers be unit-tested against a faked repository.
- Repositories live in `Persistence/Repositories/`:
  - `IRepository<TEntity>` / `Repository<TEntity>` — common operations (`GetByIdAsync`, `ListAsync`,
    `AddAsync`, `Update`, `Remove`, `SaveChangesAsync`).
  - One interface per aggregate (e.g. `IProductRepository : IRepository<Product>`) for
    entity-specific queries. Add query methods there; do **not** expose `IQueryable` (it leaks EF).
  - Register in `Program.cs`: open generic `AddScoped(typeof(IRepository<>), typeof(Repository<>))`
    plus a line per specific repo, e.g. `AddScoped<IProductRepository, ProductRepository>()`.
- **Unit of work:** every repository in a request shares the same scoped `AppDbContext`, so one
  `SaveChangesAsync()` commits all tracked changes — call it once, after the writes (even when several
  repositories were used).
- **Read trade-off (accepted):** repositories return entities, so a read maps the entity to its DTO in
  the handler rather than projecting in SQL. For a genuinely hot/heavy read, add a purpose-built method
  on the specific repository that returns the projection.
- Every async data call takes the `CancellationToken`.
- One `AppDbContext`; configure entities in `OnModelCreating` (max lengths, precision, keys).
- Migrations via the pinned local tool:
  `cd backend && dotnet ef migrations add <Name> --project src/Mtsorella.Api --output-dir Persistence/Migrations`.
  Tooling lives in `.config/dotnet-tools.json`; run `dotnet tool restore` first on a fresh clone.

## SOLID / DRY in this codebase
- **S**RP: a slice does one feature; a handler does one operation; the behavior does one cross-cutting
  concern. If a handler grows multiple responsibilities, split the slice.
- **O**CP: add a feature by adding a slice (and it auto-registers) — existing slices are untouched.
- **L**SP / **I**SP: small focused interfaces (`IEndpoint`, `IRequestHandler<,>`, `IValidator<>`);
  depend on the narrow one you need.
- **D**IP: depend on abstractions (`ISender`, `IProductRepository`/`IRepository<>`, `IValidator<>`)
  injected via DI; never `new` up dependencies inside a handler.
- **DRY**: cross-cutting logic (validation, error→HTTP mapping, endpoint registration) lives once in
  `Common/`, and data access lives behind repositories. But **do not over-DRY**: prefer a little
  duplication across slices over a shared abstraction that couples unrelated features.

## Pragmatic exceptions (intentional, documented "rule-bends")
Clean code is the default, but these deliberate deviations are accepted here — keep them, and
prefer them over "purer" alternatives that add coupling or ceremony:
- **Static-ish slice classes with nested types.** A slice bundles `Command`/`Validator`/`Handler`/
  endpoint as nested types in one class. This trades textbook one-type-per-file for high cohesion —
  it is the point of VSA.
- **Reflection at startup** in `EndpointExtensions.AddEndpoints` to discover `IEndpoint`s. Reflection
  is normally avoided, but running it once at boot to remove per-route boilerplate is a good trade.
- **A `dynamic` cast** in `ValidationBehavior` to convert `List<Error>` into the concrete `ErrorOr<T>`
  the request returns. The generic context hides the implicit conversion at compile time; the cast is
  the standard, contained workaround. Keep it isolated to that one line.
- **Localized validation messages.** FluentValidation emits messages in the host OS culture by
  default. Acceptable for now; revisit if a fixed API language is required.

## Commands
```bash
dotnet build backend/Mtsorella.slnx
dotnet test  backend/Mtsorella.slnx                 # see [[test-suite]]
dotnet run   --project backend/src/Mtsorella.Api    # /scalar, /openapi/v1.json, /health
cd backend && dotnet ef migrations add <Name> --project src/Mtsorella.Api --output-dir Persistence/Migrations
```
Adding a new feature = add one `Features/<Area>/<Feature>.cs` implementing `IEndpoint`. Nothing in
`Program.cs` changes. Add/adjust tests in the same change and keep the suite green before merge.
