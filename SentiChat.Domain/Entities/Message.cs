using SentiChat.Domain.Enums;

namespace SentiChat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }

    public Guid ChatId { get; set; }
    public Chat Chat { get; set; } = null!;

    public Guid SenderId { get; set; }
    public User Sender { get; set; } = null!;

    public string Text { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public SentimentType Sentiment { get; set; } = SentimentType.Neutral;
}
