namespace HelloWorldApp.Services;

using HelloWorldApp.Models;

internal static class OrderValidator
{
    private static readonly string[] AllowedStatuses = new[] { "Pending", "Processing", "Completed", "Cancelled" };

    public static (bool IsValid, object? Error) Validate(OrderCreateDto dto, IEnumerable<User> users)
    {
        if (dto.Total <= 0)
            return (false, new { error = "Total must be greater than 0" });

        if (!users.Any(u => u.Id == dto.UserId))
            return (false, new { error = "UserId does not exist" });

        if (dto.Status is not null && !AllowedStatuses.Contains(dto.Status))
            return (false, new { error = $"Status must be one of: {string.Join(',', AllowedStatuses)}" });

        return (true, null);
    }
}
