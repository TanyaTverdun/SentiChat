namespace SentiChat.Application.DTOs.Users;

/// <summary>
/// Data transfer object for registering a new user.
/// </summary>
public class RegisterUserDto
{
    /// <summary>
    /// User's display name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// User's email address. Must be unique
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// User's password in plain text
    /// </summary>
    public string Password { get; set; } = string.Empty;
}
