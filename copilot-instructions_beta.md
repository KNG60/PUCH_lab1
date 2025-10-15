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

Testing and verification

- Manual API smoke test: after running locally, use `curl` or your browser to visit `http://localhost:5000/api/items` (port may vary; check console output).
- Since there are no unit tests in the repository, add tests under a new test project (e.g., `HelloWorldApp.Tests`) if desired.

Conventions and cautions for AI agents

- Keep edits minimal and localize changes to `Program.cs` unless adding a new source file or project.
- Preserve the minimal API structure and the in-memory `items` list unless the task explicitly requires persistence.
- Do not remove `ImplicitUsings` or `Nullable` project settings in the `.csproj` unless necessary; they reflect the project's coding style.

Examples of good PRs

- Small focused change: add a new endpoint `/api/health` that returns `Ok()`.
- Add input validation to `app.MapPost("/api/items", ...)` by checking `dto.Name` and returning `BadRequest` when missing.

If anything here is unclear or you'd like the instructions to cover additional workflows (CI, tests, or database migration steps), tell me which areas to expand.