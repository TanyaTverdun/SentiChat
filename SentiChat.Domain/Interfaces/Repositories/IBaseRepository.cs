namespace SentiChat.Domain.Interfaces.Repositories;

/// <summary>
/// Base generic repository for common data operations.
/// </summary>
/// <typeparam name="T">The entity type.</typeparam>
public interface IBaseRepository<T> where T : class
{
    /// <summary>
    /// Adds a new entity to the data store.
    /// </summary>
    Task AddAsync(T entity, CancellationToken cancellationToken);
}
