namespace SentiChat.Application.DTOs.Chats;

/// <summary>
/// Represents the data required to open or create a personal chat.
/// </summary>
public record CreatePersonalChatRequestDto
{
    /// <summary>
    /// The unique identifier of the user with whom the personal chat will be initiated.
    /// </summary>
    public string TargetUserEmail { get; init; } = string.Empty;
}
