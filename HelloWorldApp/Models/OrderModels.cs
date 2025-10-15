namespace HelloWorldApp.Models;

public record OrderCreateDto
{
    public int UserId { get; init; }
    public decimal Total { get; init; }
    public string? Status { get; init; }
}

public record User
{
    public int Id { get; init; }
    public string? Username { get; init; }
}
