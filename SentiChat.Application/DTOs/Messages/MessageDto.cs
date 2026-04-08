namespace SentiChat.Application.DTOs.Messages;

/// <summary>
/// Represents a single message within a chat, including its content and sentiment analysis results.
/// </summary>
public record MessageDto
{
    /// <summary>
    /// Unique identifier of the message.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Unique identifier of the user who sent the message. 
    /// Used by the UI to determine message alignment (left/right).
    /// </summary>
    public Guid SenderId { get; init; }

    /// <summary>
    /// The text content of the message.
    /// </summary>
    public string Content { get; init; } = string.Empty;

    /// <summary>
    /// The date and time when the message was sent.
    /// </summary>
    public DateTime SentAt { get; init; }

    /// <summary>
    /// The sentiment of the message (e.g., "Positive", "Negative", "Neutral", "Mixed"). 
    /// Used by the UI to apply specific styling or icons.
    /// </summary>
    public string Sentiment { get; init; } = "Neutral";
}
