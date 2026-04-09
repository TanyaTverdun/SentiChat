using SentiChat.Application.DTOs.Chats;

namespace SentiChat.Application.Interfaces;

/// <summary>
/// Defines a contract for managing chat-related business logic, 
/// such as retrieving chat lists and creating new conversations.
/// </summary>
public interface IChatService
{
    /// // <summary>
    /// Retrieves a list of chats for a specific user, 
    /// formatted for display on the main chat screen.
    /// Automatically resolves the chat name 
    /// (e.g., the other user's name for personal chats) and the last message.
    /// </summary>
    /// <param name="userId">
    /// The unique identifier of the user requesting their chats.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A collection of chat data transfer objects (DTOs), typically sorted by the most recent message.
    /// </returns>
    Task<IEnumerable<ChatListItemDto>> GetUserChatsAsync(
        Guid userId,
        CancellationToken cancellationToken);

    /// <summary>
    /// Finds an existing personal (1-on-1) chat between two users, 
    /// or creates a new one if no prior chat exists.
    /// Used when a user clicks "Message" on another user's profile.
    /// </summary>
    /// <param name="currentUserId">
    /// The unique identifier of the first user (usually the current user).
    /// </param>
    /// <param name="userEmail">
    /// The unique identifier of the second user (the target user).
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The unique identifier (Guid) of the existing or newly created chat.
    /// </returns>
    Task<Guid> GetOrCreatePersonalChatAsync(
        Guid currentUserId,
        string userEmail,
        CancellationToken cancellationToken);
}
