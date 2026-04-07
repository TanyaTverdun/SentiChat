namespace SentiChat.Application.Interfaces.Security;

/// <summary>
/// Abstraction for password hashing and verification.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes a plain text password.
    /// </summary>
    string HashPassword(string password);

    /// <summary>
    /// Verifies that a plain text password matches the hashed password.
    /// </summary>
    bool VerifyPassword(string hashedPassword, string providedPassword);
}
