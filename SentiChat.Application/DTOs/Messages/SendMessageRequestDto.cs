namespace SentiChat.Application.DTOs.Messages;

/// <summary>
/// Represents the data required to send a new message.
/// </summary>
public record SendMessageRequestDto
{
    /// <summary>
    /// The text content of the message to be sent.
    /// </summary>
    public string Content { get; init; } = string.Empty;
}
