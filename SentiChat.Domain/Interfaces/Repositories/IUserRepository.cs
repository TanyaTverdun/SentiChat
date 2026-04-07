using SentiChat.Domain.Entities;

namespace SentiChat.Domain.Interfaces.Repositories;

/// <summary>
/// Repository interface for User entity operations.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Checks if a user with the specified email already exists.
    /// </summary>
    /// <param name="email">The email address to check.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>True if found; otherwise, false.</returns>
    Task<bool> ExistsByEmailAsync(
        string email, 
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">The email address to search for.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The user entity if found; otherwise, null.</returns>
    Task<User?> GetByEmailAsync(
        string email, 
        CancellationToken cancellationToken);
}
