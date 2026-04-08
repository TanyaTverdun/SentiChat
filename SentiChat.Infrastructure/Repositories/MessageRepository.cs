using Microsoft.EntityFrameworkCore;
using SentiChat.Domain.Entities;
using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Infrastructure.Data;

namespace SentiChat.Infrastructure.Repositories;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(SentiChatDbContext context) 
        : base(context)
    {
    }

    public async Task<IEnumerable<Message>> GetMessagesAsync(
        Guid chatId, 
        int pageSize, 
        DateTime? before, 
        CancellationToken cancellationToken)
    {
        IQueryable<Message> query = this._dbSet
            .Where(m => m.ChatId == chatId);

        if (before.HasValue)
        {
            query = query.Where(m => m.CreatedAt < before.Value);
        }

        return await query
            .OrderByDescending(m => m.CreatedAt)
            .Take(pageSize)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
