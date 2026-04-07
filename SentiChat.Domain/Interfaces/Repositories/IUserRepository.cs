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
    public Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken);
}
