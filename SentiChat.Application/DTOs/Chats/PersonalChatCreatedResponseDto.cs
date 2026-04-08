namespace SentiChat.Application.DTOs.Chats;

/// <summary>
/// Represents the data required to open or create a personal chat.
/// </summary>
public record PersonalChatCreatedResponseDto
{
    /// <summary>
    /// The unique identifier of the user with whom the personal chat will be initiated.
    /// </summary>
    public Guid ChatId { get; init; }
}
