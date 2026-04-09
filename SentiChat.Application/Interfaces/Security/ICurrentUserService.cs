namespace SentiChat.Application.Interfaces.Security;

/// <summary>
/// Provides access to the current authenticated user's information.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the unique identifier (GUID) 
    /// of the currently authenticated user.
    /// </summary>
    /// <returns>The user ID.</returns>
    /// <exception cref="UnauthorizedAccessException">
    /// Thrown when the user is not authenticated 
    /// or the ID cannot be parsed from the token.
    /// </exception>
    Guid GetUserId();
}
