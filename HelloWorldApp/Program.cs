using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => Results.Ok(new { message = "Hello from HelloWorldApp API" }));

app.MapGet("/api/hello", () => Results.Ok("Hello API user!"));


// .NET/runtime/spec information endpoint
app.MapGet("/api/spec", () =>
{
	var entryVersion = Assembly.GetEntryAssembly()?.GetName().Version?.ToString();
	var spec = new
	{
		Framework = RuntimeInformation.FrameworkDescription,
		TargetFramework = AppContext.TargetFrameworkName,
		OS = RuntimeInformation.OSDescription,
		OSArchitecture = RuntimeInformation.OSArchitecture.ToString(),
		ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString(),
		ClrVersion = Environment.Version.ToString(),
		EntryAssemblyVersion = entryVersion,
		MachineName = Environment.MachineName,
		ProcessId = Process.GetCurrentProcess().Id
	};

	return Results.Ok(spec);
});

// Simple in-memory store
var items = new List<Item>()
{
	new Item { Id = 1, Name = "Item 1", Description = "First item" },
	new Item { Id = 2, Name = "Item 2", Description = "Second item" }
};

// In-memory contacts store
var contacts = new List<ContactSubmission>();

// Contact form page (simple HTML)
app.MapGet("/contact", () => Results.Content(@"<!doctype html>
<html>
	<head>
		<meta charset='utf-8'/>
		<title>Contact</title>
	</head>
	<body>
		<h1>Contact Us</h1>
		<form method='post' action='/contact'>
			<label>Name:<br/><input type='text' name='name' required/></label><br/>
			<label>Email:<br/><input type='email' name='email' required/></label><br/>
			<label>Message:<br/><textarea name='message' rows='6' cols='40' required></textarea></label><br/>
			<button type='submit'>Send</button>
		</form>
	</body>
</html>", "text/html"));

// Accept contact submissions from form (application/x-www-form-urlencoded) or JSON
app.MapPost("/contact", async (HttpContext http) =>
{
		if (http.Request.HasFormContentType)
		{
				var form = await http.Request.ReadFormAsync();
				var name = form["name"].ToString();
				var email = form["email"].ToString();
				var message = form["message"].ToString();
				var submission = new ContactSubmission { Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1, Name = name, Email = email, Message = message, ReceivedAt = DateTime.UtcNow };
				contacts.Add(submission);
				// Simple thank you page
				return Results.Content($"<html><body><h1>Thanks, {System.Net.WebUtility.HtmlEncode(name)}!</h1><p>Your message was received.</p><p><a href='/contact'>Back</a></p></body></html>", "text/html");
		}

		// Try to bind JSON
		try
		{
				var dto = await http.Request.ReadFromJsonAsync<ContactCreateDto>();
				if (dto is null) return Results.BadRequest();
				var submission = new ContactSubmission { Id = contacts.Any() ? contacts.Max(c => c.Id) + 1 : 1, Name = dto.Name, Email = dto.Email, Message = dto.Message, ReceivedAt = DateTime.UtcNow };
				contacts.Add(submission);
				return Results.Created($"/api/contacts/{submission.Id}", submission);
		}
		catch
		{
				return Results.BadRequest();
		}
});

app.MapGet("/api/contacts", () => Results.Ok(contacts));

app.MapGet("/api/items", () => Results.Ok(items));

app.MapGet("/api/items/{id:int}", (int id) =>
{
	var item = items.FirstOrDefault(i => i.Id == id);
	return item is not null ? Results.Ok(item) : Results.NotFound();
});

app.MapPost("/api/items", (ItemCreateDto dto) =>
{
	var nextId = items.Any() ? items.Max(i => i.Id) + 1 : 1;
	var item = new Item { Id = nextId, Name = dto.Name, Description = dto.Description };
	items.Add(item);
	return Results.Created($"/api/items/{item.Id}", item);
});

app.MapPut("/api/items/{id:int}", (int id, ItemCreateDto dto) =>
{
	var item = items.FirstOrDefault(i => i.Id == id);
	if (item is null) return Results.NotFound();
	item.Name = dto.Name;
	item.Description = dto.Description;
	return Results.NoContent();
});

app.MapDelete("/api/items/{id:int}", (int id) =>
{
	var item = items.FirstOrDefault(i => i.Id == id);
	if (item is null) return Results.NotFound();
	items.Remove(item);
	return Results.NoContent();
});

// In-memory users store and CRUD endpoints
var users = new List<User>()
{
	new User { Id = 1, Username = "alice", Email = "alice@example.com" },
	new User { Id = 2, Username = "bob", Email = "bob@example.com" }
};

app.MapGet("/api/users", () => Results.Ok(users));

// Search users by username (case-insensitive, partial match)
app.MapGet("/api/users/search", (string? username) =>
{
	if (string.IsNullOrWhiteSpace(username)) return Results.BadRequest(new { error = "username query is required" });
	var matches = users.Where(u => u.Username is not null && u.Username.Contains(username, StringComparison.OrdinalIgnoreCase)).ToList();
	return Results.Ok(matches);
});

app.MapGet("/api/users/{id:int}", (int id) =>
{
	var user = users.FirstOrDefault(u => u.Id == id);
	return user is not null ? Results.Ok(user) : Results.NotFound();
});

app.MapPost("/api/users", (UserCreateDto dto) =>
{
	if (string.IsNullOrWhiteSpace(dto.Username)) return Results.BadRequest(new { error = "Username is required" });
	if (users.Any(u => string.Equals(u.Username, dto.Username, StringComparison.OrdinalIgnoreCase)))
		return Results.Conflict(new { error = "Username already exists" });

	var nextId = users.Any() ? users.Max(u => u.Id) + 1 : 1;
	var user = new User { Id = nextId, Username = dto.Username, Email = dto.Email };
	users.Add(user);
	return Results.Created($"/api/users/{user.Id}", user);
});

app.MapPut("/api/users/{id:int}", (int id, UserCreateDto dto) =>
{
	var user = users.FirstOrDefault(u => u.Id == id);
	if (user is null) return Results.NotFound();
	if (string.IsNullOrWhiteSpace(dto.Username)) return Results.BadRequest(new { error = "Username is required" });
	// Prevent changing to a username that another user already has
	if (users.Any(u => u.Id != id && string.Equals(u.Username, dto.Username, StringComparison.OrdinalIgnoreCase)))
		return Results.Conflict(new { error = "Username already exists" });

	user.Username = dto.Username;
	user.Email = dto.Email;
	return Results.NoContent();
});

app.MapDelete("/api/users/{id:int}", (int id) =>
{
	var user = users.FirstOrDefault(u => u.Id == id);
	if (user is null) return Results.NotFound();
	users.Remove(user);
	return Results.NoContent();
});

app.Run();

internal record Item
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Description { get; set; }
}

internal record ItemCreateDto(string? Name, string? Description);

internal record ContactSubmission
{
	public int Id { get; set; }
	public string? Name { get; set; }
	public string? Email { get; set; }
	public string? Message { get; set; }
	public DateTime ReceivedAt { get; set; }
}

internal record ContactCreateDto(string? Name, string? Email, string? Message);

internal record User
{
	public int Id { get; set; }
	public string? Username { get; set; }
	public string? Email { get; set; }
}

internal record UserCreateDto(string? Username, string? Email);
