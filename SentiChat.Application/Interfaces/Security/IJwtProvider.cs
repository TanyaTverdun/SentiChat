using SentiChat.Domain.Entities;

namespace SentiChat.Application.Interfaces.Security;

/// <summary>
/// Defines a contract for authentication token generation.
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Generates a JSON Web Token (JWT) for the specified user.
    /// </summary>
    /// <param name="user">
    /// The user entity containing the data to be embedded 
    /// in the token (e.g., Id, Email).
    /// </param>
    /// <returns>
    /// A string representation of the generated JWT.
    /// </returns>
    string GenerateToken(User user);
}
