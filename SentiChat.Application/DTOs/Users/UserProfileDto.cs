namespace SentiChat.Application.DTOs.Users;

/// <summary>
/// Represents the user's public profile data.
/// </summary>
public record UserProfileDto
{
    /// <summary>
    /// The unique identifier of the user.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// The user's full name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The user's email address.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// A short "About me" section.
    /// </summary>
    public string? Bio { get; init; }

    /// <summary>
    /// Generated initials based on the user's name (e.g., "ОШ" for "Олена Шевченко").
    /// </summary>
    public string Initials { get; init; } = string.Empty;
}
