using Riok.Mapperly.Abstractions;
using SentiChat.Application.DTOs.Messages;
using SentiChat.Domain.Entities;
using SentiChat.Domain.Enums;

namespace SentiChat.Application.Mappers;

/// <summary>
/// Provides mapping methods for 
/// Message-related entities using Riok.Mapperly.
/// </summary>
[Mapper]
public static partial class MessageMapper
{
    /// <summary>
    /// Creates a new <see cref="Message"/> 
    /// entity from individual components.
    /// This is a manual factory method used after 
    /// sentiment analysis and ID generation.
    /// </summary>
    /// <param name="id">
    /// The unique identifier for the new message.
    /// </param>
    /// <param name="chatId">
    /// The ID of the chat this message belongs to.
    /// </param>
    /// <param name="senderId">
    /// The ID of the user who sent the message.
    /// </param>
    /// <param name="content">
    /// The actual text content of the message.
    /// </param>
    /// <param name="createdAt">
    /// The timestamp when the message was processed on the server.
    /// </param>
    /// <param name="sentiment">
    /// The AI-analyzed sentiment of the message.
    /// </param>
    /// <returns>
    /// A fully constructed <see cref="Message"/> 
    /// entity ready to be saved to the database.
    /// </returns>
    public static Message ToEntity(
        Guid id,
        Guid chatId,
        Guid senderId,
        string content,
        DateTime createdAt,
        SentimentType sentiment)
    {
        return new Message
        {
            Id = id,
            ChatId = chatId,
            SenderId = senderId,
            Text = content,
            CreatedAt = createdAt,
            Sentiment = sentiment
        };
    }

    /// <summary>
    /// Maps a <see cref="Message"/> entity to a <see cref="MessageDto"/>.
    /// Automatically handles property name translations 
    /// (e.g., Text -> Content, CreatedAt -> SentAt).
    /// </summary>
    /// <param name="message">
    /// The source message entity.
    /// </param>
    /// <returns>
    /// A fully mapped <see cref="MessageDto"/>.
    /// </returns>
    [MapperIgnoreSource(nameof(Message.Chat))]
    [MapperIgnoreSource(nameof(Message.ChatId))]
    [MapperIgnoreSource(nameof(Message.Sender))]
    [MapProperty(nameof(Message.Text), nameof(MessageDto.Content))]
    [MapProperty(nameof(Message.CreatedAt), nameof(MessageDto.SentAt))]
    [MapProperty(nameof(Message.Sentiment), nameof(MessageDto.Sentiment))]
    public static partial MessageDto ToDto(this Message message);

    /// <summary>
    /// Maps a collection of <see cref="Message"/> 
    /// entities to a collection of <see cref="MessageDto"/>s.
    /// </summary>
    /// <param name="messages">
    /// The source collection of messages.
    /// </param>
    /// <returns>
    /// An enumerable of mapped DTOs.
    /// </returns>
    public static partial IEnumerable<MessageDto> ToDtoList(
        this IEnumerable<Message> messages);

    private static string MapSentimentToString(
        SentimentType sentiment) => sentiment.ToString();
}
