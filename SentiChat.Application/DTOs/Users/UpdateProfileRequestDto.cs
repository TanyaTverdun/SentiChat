namespace SentiChat.Application.DTOs.Users;

/// <summary>
/// The data required to update a user's profile.
/// </summary>
public record UpdateProfileRequestDto
{
    /// <summary>
    /// The user's new full name.
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// The user's new email address.
    /// </summary>
    public string Email { get; init; } = string.Empty;

    /// <summary>
    /// A short "About me" section.
    /// </summary>
    public string? Bio { get; init; }
}
