namespace SentiChat.Application.DTOs.Users;

/// <summary>
/// Data transfer object for user authentication.
/// </summary>
public class LoginUserDto
{
    /// <summary>
    /// The email address associated with the user account.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The plain-text password for authentication.
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
