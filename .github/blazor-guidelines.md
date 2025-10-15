# Blazor Guidelines for HelloWorldApp

This repo is a tiny minimal API; these notes explain how to safely add a small Blazor frontend (WASM or Server) that integrates with the API.

1. Project type
- Prefer Blazor WASM hosted (3-project template) or a separate Blazor Server project depending on auth needs. For small demos, Blazor WASM calling the API is simplest.

2. Structure
- Create a new folder `HelloWorldApp.Client` for the Blazor project (or `HelloWorldApp.Server` for server-side).
- Keep API surface in `HelloWorldApp` (the minimal API project). The client should only call the API via HTTP.

3. CORS and hosting
- Enable CORS in `Program.cs` of `HelloWorldApp` when serving a separate client: add `builder.Services.AddCors()` and `app.UseCors(..)` with a restrictive origin during development (e.g., `http://localhost:5001`).

4. HTTP calls
- Use `HttpClient` injected into Blazor (via `Program.cs` in the client project) and call endpoints like `/api/items`, `/api/users`.
- Handle API responses using pattern: if (!response.IsSuccessStatusCode) { show error } else { parse JSON }.

5. Authentication
- This sample has no auth. If adding auth, prefer cookie or JWT depending on hosting model and keep auth logic in the API.

6. Development workflow
- Run the API locally (`dotnet run --project HelloWorldApp`).
- Run the Blazor client with `dotnet run --project HelloWorldApp.Client` (or `npm`/dev server if applicable).
- Use the browser devtools to inspect CORS and network calls.

7. Examples
- Example `HttpClient` call in Blazor:
  var users = await Http.GetFromJsonAsync<List<User>>("/api/users");

8. Small UX/UXs
- The API endpoints return simple JSON shapes; design UIs to handle empty lists and server validation errors (400/409) by showing user-friendly messages.

If you'd like, I can scaffold a minimal `HelloWorldApp.Client` Blazor WASM project and wire one page to `/api/items` for demo purposes.