namespace SentiChat.Domain.Interfaces.Repositories;

/// <summary>
/// Interface for managing database transactions.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves changes to the database.
    /// </summary>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
