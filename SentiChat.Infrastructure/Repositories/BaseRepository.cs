using Microsoft.EntityFrameworkCore;
using SentiChat.Domain.Interfaces.Repositories;

namespace SentiChat.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected readonly DbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public BaseRepository(DbContext dbContext)
    {
        this._dbContext = dbContext;
        this._dbSet = _dbContext.Set<T>();
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await this._dbSet.AddAsync(entity, cancellationToken);
    }
}
