using SentiChat.Domain.Entities;

namespace SentiChat.Domain.Interfaces.Repositories;

public interface IChatRepository
{
    /// <summary>
    /// Retrieves all chats associated with a specific user. 
    /// Includes navigation properties such as chat members and messages.
    /// </summary>
    /// <param name="userId">The unique identifier of the user.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of chats the user is a member of.</returns>
    Task<IEnumerable<Chat>> GetUserChatsAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a personal (1-on-1) chat between two specific users, if it exists.
    /// Specifically filters for chats where IsGroup is false.
    /// </summary>
    /// <param name="user1Id">The unique identifier of the first user.</param>
    /// <param name="user2Id">The unique identifier of the second user.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The personal chat entity if found; otherwise, null.</returns>
    Task<Chat?> GetPersonalChatBetweenUsersAsync(Guid user1Id, Guid user2Id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a specific chat by its ID, including all its details (members and messages).
    /// </summary>
    /// <param name="chatId">The unique identifier of the chat.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The chat entity if found; otherwise, null.</returns>
    Task<Chat?> GetChatByIdAsync(Guid chatId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a newly created chat to the database.
    /// </summary>
    /// <param name="chat">The chat entity to add.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    Task AddAsync(Chat chat, CancellationToken cancellationToken = default);
}
