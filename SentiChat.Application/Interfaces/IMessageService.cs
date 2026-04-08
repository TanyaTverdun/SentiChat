using SentiChat.Application.DTOs.Messages;

namespace SentiChat.Application.Interfaces;

/// <summary>
/// Defines a contract for message-related business logic, such as sending messages with sentiment analysis 
/// and retrieving paginated message history.
/// </summary>
public interface IMessageService
{
    /// <summary>
    /// Sends a message, triggers sentiment analysis, and saves it to the database.
    /// </summary>
    /// <param name="chatId">The unique identifier of the chat where the message is sent.</param>
    /// <param name="senderId">The unique identifier of the user sending the message.</param>
    /// <param name="content">The text content of the message.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>
    /// A tuple containing:
    /// 1. A <see cref="MessageDto"/> representing the saved message with its sentiment.
    /// 2. A collection of string identifiers representing the other chat members to notify.
    /// </returns>
    /// <exception cref="KeyNotFoundException">Thrown when the specified chat or sender does not exist.</exception>
    /// <exception cref="ArgumentException">Thrown when the message content is null or empty.</exception>
    Task<(MessageDto Message, IEnumerable<string> ReceiverIds)> SendMessageAsync(
        Guid chatId,
        Guid senderId,
        string content,
        CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves a paginated history of messages for a specific chat.
    /// </summary>
    /// <param name="chatId">The unique identifier of the chat.</param>
    /// <param name="pageSize">The maximum number of messages to retrieve in one request.</param>
    /// <param name="before">
    /// An optional timestamp to retrieve messages older than this date (used for infinite scrolling/pagination). 
    /// If null, the most recent messages are returned.
    /// </param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of <see cref="MessageDto"/> objects, sorted chronologically.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the specified chat does not exist.</exception>
    Task<IEnumerable<MessageDto>> GetChatHistoryAsync(
        Guid chatId,
        int pageSize,
        DateTime? before,
        CancellationToken cancellationToken);
}
