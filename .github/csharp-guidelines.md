# C# Guidelines for HelloWorldApp

This file lists concise, repo-specific C# conventions and examples to help contributors and AI agents.

1. Project style
- Single small ASP.NET Core minimal API project. Keep changes small and local to `Program.cs` for exercises.
- Target framework: net9.0 (see `HelloWorldApp/HelloWorldApp.csproj`).
- `ImplicitUsings` and `Nullable` are enabled — prefer nullable-aware code.

2. Naming & files
- Types (classes/records): PascalCase (e.g., `Order`, `OrderCreateDto`).
- Methods and properties: PascalCase. Local variables: camelCase.
- Place small domain models under `HelloWorldApp/Models` when they grow beyond simple records in `Program.cs`.

3. DTOs & minimal API patterns
- Use small records for DTOs (e.g., `internal record ItemCreateDto(string? Name, string? Description);`).
- Map endpoints directly in `Program.cs` with `app.MapGet/MapPost/MapPut/MapDelete` handlers.
- Return API results using `Results.Ok()`, `Results.Created()`, `Results.BadRequest()`, `Results.NotFound()`, `Results.NoContent()`, `Results.Conflict()`.

4. Validation
- Keep validation logic in small services under `HelloWorldApp/Services` (e.g., `OrderValidator`).
- Validators should return a simple shape (bool + error object) so handlers can map responses to `Results.*`.

5. Dependency guidance
- No DI containers are configured beyond minimal built-ins. If you add services, register them in `builder.Services` and update `.csproj` if new packages are used.

6. Tests and CI (recommendation)
- If adding tests, create `HelloWorldApp.Tests` (xUnit) and prefer integration-style tests that run the minimal API using `WebApplicationFactory`.
- CI should run `dotnet build` and `dotnet test` for added tests.

7. Examples
- Minimal POST validation pattern in `Program.cs`:
  - Validate DTO using a service. If invalid, return `Results.BadRequest(error)`.
  - On conflict, return `Results.Conflict()`.
  - On success, return `Results.Created($"/api/resource/{id}", resource)`.

8. Small PR guidance
- Keep PRs focused (single endpoint or small refactor).
- Document any changes to `Program.cs` routes in the PR description.

If you'd like, I can add a sample `HelloWorldApp.Tests` project scaffold or a GitHub Actions workflow for builds/tests.