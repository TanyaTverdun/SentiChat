using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Infrastructure.Data;

namespace SentiChat.Infrastructure.Repositories;

/// <summary>
/// Provides the standard implementation of <see cref="IUnitOfWork"/>.
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly SentiChatDbContext _dbContext;

    public UnitOfWork(SentiChatDbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await this._dbContext.SaveChangesAsync(cancellationToken);
    }
}
