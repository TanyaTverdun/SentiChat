namespace SentiChat.Application.DTOs.Chats;

/// <summary>
/// Represents a summary of a chat for display in a list.
/// </summary>
public class ChatListItemDto
{
    /// <summary>
    /// The unique identifier for the chat.
    /// </summary>
    public Guid ChatId { get; set; }

    /// <summary>
    /// The display name of the chat (e.g., recipient's name or group title).
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The initials to be displayed in the chat's avatar.
    /// </summary>
    public string Initials { get; set; } = string.Empty;

    /// <summary>
    /// The text content of the last message sent in this chat.
    /// </summary>
    public string? LastMessageText { get; set; }

    /// <summary>
    /// The timestamp of the last message.
    /// </summary>
    public DateTime? LastMessageTime { get; set; }
}
