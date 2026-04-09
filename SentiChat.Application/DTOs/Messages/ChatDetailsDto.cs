namespace SentiChat.Application.DTOs.Messages;

/// <summary>
/// Represents the full details of a chat, including 
/// participant information and message history.
/// </summary>
public record ChatDetailsDto
{
    /// <summary>
    /// Unique identifier of the chat.
    /// </summary>
    public Guid ChatId { get; init; }

    /// <summary>
    /// The display name of the chat (e.g., the partner's name or group name). 
    /// Displayed in the chat header.
    /// </summary>
    public string ChatName { get; init; } = string.Empty;

    /// <summary>
    /// Initials of the target user (e.g., "IM"). 
    /// Used for the avatar placeholder.
    /// </summary>
    public string TargetUserInitials { get; init; } = string.Empty;

    /// <summary>
    /// The collection of messages in this chat, 
    /// typically ordered chronologically.
    /// </summary>
    public IEnumerable<MessageDto> Messages { get; init; } 
        = new List<MessageDto>();
}
