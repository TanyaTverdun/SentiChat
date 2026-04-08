using Microsoft.EntityFrameworkCore;
using SentiChat.Domain.Entities;
using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Infrastructure.Data;

namespace SentiChat.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(SentiChatDbContext dbContext) 
        : base(dbContext)
    {
    }

    public async Task<bool> ExistsByEmailAsync(
        string email, 
        CancellationToken cancellationToken)
    {
        return await this._dbSet
            .AnyAsync(
                u => u.Email == email, 
                cancellationToken);
    }

    public async Task<User?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await this._dbSet
            .FirstOrDefaultAsync(
                u => u.Email == email,
                cancellationToken);
    }

    public async Task<User?> GetByIdAsync(
        Guid id, 
        CancellationToken cancellationToken)
    {
        return await this._dbSet
            .FirstOrDefaultAsync(
                u => u.Id == id, 
                cancellationToken);
    }
}
