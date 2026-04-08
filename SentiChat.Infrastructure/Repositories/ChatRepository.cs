using Microsoft.EntityFrameworkCore;
using SentiChat.Domain.Entities;
using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Infrastructure.Data;

namespace SentiChat.Infrastructure.Repositories;

public class ChatRepository : BaseRepository<Chat>, IChatRepository
{
    public ChatRepository(SentiChatDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<Chat>> GetUserChatsAsync(
        Guid userId, 
        CancellationToken cancellationToken)
    {
        return await this._dbSet
            .Include(c => c.Members)
                .ThenInclude(cm => cm.User)
            .Include(c => c.Messages)
            .Where(c => c.Members.Any(m => m.UserId == userId))
            .ToListAsync(cancellationToken);
    }

    public async Task<Chat?> GetPersonalChatBetweenUsersAsync(
        Guid user1Id, 
        Guid user2Id, 
        CancellationToken cancellationToken)
    {
        return await this._dbSet
            .Where(c => !c.IsGroup)
            .Where(c => c.Members.Any(m => m.UserId == user1Id))
            .Where(c => c.Members.Any(m => m.UserId == user2Id))
            .Include(c => c.Members)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Chat?> GetChatByIdAsync(
        Guid chatId,
        CancellationToken cancellationToken)
    {
        return await this._dbSet
            .Include(c => c.Members)
                .ThenInclude(cm => cm.User)
            .Include(c => c.Messages)
            .FirstOrDefaultAsync(c => c.Id == chatId, cancellationToken);
    }
}
