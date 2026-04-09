using SentiChat.Application.Interfaces.Security;

namespace SentiChat.Infrastructure.Security;

/// <summary>
/// Provides the standard implementation of <see cref="IPasswordHasher"/>
/// </summary>
public class PasswordHasher : IPasswordHasher
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    /// <inheritdoc />
    public bool VerifyPassword(
        string hashedPassword, 
        string providedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(
            providedPassword, 
            hashedPassword);
    }
}
