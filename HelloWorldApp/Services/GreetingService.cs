namespace HelloWorldApp.Services;

internal class GreetingService
{
    /// <summary>
    /// Returns a simple greeting. If name is provided, returns a personalized greeting.
    /// </summary>
    public string GetGreeting(string? name = null)
    {
        return string.IsNullOrWhiteSpace(name) ? "Hello from GreetingService!" : $"Hello, {name}!";
    }
}

