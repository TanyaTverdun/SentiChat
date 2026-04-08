namespace SentiChat.Application.DTOs.Users;

/// <summary>
/// Represents the successful authentication response.
/// </summary>
public record AuthResponseDto
{
    /// <summary>
    /// The generated JSON Web Token (JWT) for the authenticated session.
    /// </summary>
    public string Token { get; init; } = string.Empty;
}
