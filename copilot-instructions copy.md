# Copilot instructions for HelloWorldApp

This project is a minimal ASP.NET Core "minimal API" sample (single project) used for exercises. The file `HelloWorldApp/Program.cs` contains the entire application and is the primary editing surface.

Key facts and architecture

- Single ASP.NET Core Web application targeting .NET 9 (`HelloWorldApp/HelloWorldApp.csproj`).
- Program is implemented as a minimal API in `Program.cs` with route mappings like `/api/items` and `/api/hello`.
- State is in-process/in-memory: an `items` List<Item> lives in `Program.cs`. There is no database.
- No DI beyond built-in services: only `AddEndpointsApiExplorer` is used.

Primary developer workflows

- Build solution: `dotnet build repo.sln`
- Run locally: `dotnet run --project HelloWorldApp` (or use `dotnet run` in `HelloWorldApp` folder).
- Debug: open the project in Visual Studio or VS Code (with C# extension) and run the project; `Program.cs` is the entry point.

Project-specific patterns and examples

- Minimal API style: route handlers are declared with `app.MapGet`, `app.MapPost`, `app.MapPut`, `app.MapDelete`.
  - Example: `app.MapGet("/api/items/{id:int}", (int id) => { ... });`
- DTOs are small internal records defined in `Program.cs` (e.g., `Item` and `ItemCreateDto`). Edit these in-place for small experiments.
- Persistence: since items are stored in an in-memory `List<Item>`, restarting the app resets state. Use this for tests or extend to a database if persistence is needed.

Integration and external dependencies

- No external packages or services are referenced in the project file. Everything is contained in the single project.

Where to make changes

- For new endpoints or small feature experiments, edit `HelloWorldApp/Program.cs`.
- For adding packages, update `HelloWorldApp/HelloWorldApp.csproj` and run `dotnet restore` (restores automatically on build).

# Copilot instructions for HelloWorldApp (concise)

This repository contains a minimal ASP.NET Core 9 "minimal API" sample implemented as a single project: `HelloWorldApp`.

Core facts (big-picture)
- Single web app (target: net9.0). Entry point: `HelloWorldApp/Program.cs`.
- The app uses the minimal API style (route handlers declared with app.MapGet/MapPost/MapPut/MapDelete).
- Application state is in-process and ephemeral: `items` is a List<Item> in `Program.cs`. No database or external services.

Quick developer workflows (concrete)
- Build solution: dotnet build repo.sln
- Run locally: dotnet run --project HelloWorldApp
- Port: defaults are printed by ASP.NET on start; try http://localhost:5000 or the URL shown in console.
- Debug: open project in VS/VS Code (C# extension); `Program.cs` is the main file to set breakpoints in.

Project-specific patterns and examples
- Keep changes localized to `Program.cs` for small experiments (adding endpoints, modifying DTOs).
  - Example endpoint: app.MapGet("/api/items/{id:int}", (int id) => { ... });
- DTOs and small records are defined inline in `Program.cs` (see `Item` and `ItemCreateDto`).
- No DI containers are configured beyond built-in services; prefer small, local edits for exercises.

Integration & dependencies
- No external NuGet packages are referenced. To add one, update `HelloWorldApp/HelloWorldApp.csproj` and run `dotnet restore` (build will restore automatically).

Conventions & cautions for AI agents
- Preserve minimal API structure and in-memory state unless asked to persist data.
- Avoid large refactors across files; propose them first in the PR description.
- Keep public API stable for exercises (route names like `/api/items` are part of expected behavior).
- Respect project settings in `HelloWorldApp.csproj` (`ImplicitUsings` enabled, `Nullable` enabled).

Useful edits and examples
- Add a health endpoint: `app.MapGet("/api/health", () => Results.Ok());`
- Example small improvement: validate `ItemCreateDto` in POST handler and return `Results.BadRequest()` when `Name` is null/empty.

Files to inspect first
- `HelloWorldApp/Program.cs` — primary editing surface
- `HelloWorldApp/HelloWorldApp.csproj` — target/framework and project settings

If something is underspecified
- Assume minimal changes are preferred. If persistence or DI is required, ask whether to add a new project (for tests) or extend this project.

If you'd like, I can:
- Add a `/api/greet` endpoint that uses the new `HelloWorldApp/Services/GreetingService.cs` class.
- Scaffold a test project `HelloWorldApp.Tests` with a couple of integration tests.

Please review and tell me which additional workflows or constraints to include (CI, code style, PR process).