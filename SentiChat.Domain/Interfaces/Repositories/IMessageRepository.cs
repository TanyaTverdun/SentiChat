using SentiChat.Domain.Entities;
using System.Data;

namespace SentiChat.Domain.Interfaces.Repositories;

/// <summary>
/// Defines a contract for data access 
/// operations related to message entities.
/// </summary>
public interface IMessageRepository : IBaseRepository<Message>
{
    /// <summary>
    /// Retrieves a paginated list of messages 
    /// for a specific chat, ordered chronologically.
    /// </summary>
    /// <param name="chatId">
    /// The unique identifier of the chat.
    /// </param>
    /// <param name="pageSize">
    /// The number of messages to retrieve.
    /// </param>
    /// <param name="before">
    /// The timestamp of the oldest message currently 
    /// displayed (for loading older messages).
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A list of messages for the chat.
    /// </returns>
    Task<IEnumerable<Message>> GetMessagesAsync(
        Guid chatId,
        int pageSize,
        DateTime? before,
        CancellationToken cancellationToken);
}
