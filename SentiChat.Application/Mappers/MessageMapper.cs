using Riok.Mapperly.Abstractions;
using SentiChat.Application.DTOs.Messages;
using SentiChat.Domain.Entities;
using SentiChat.Domain.Enums;

namespace SentiChat.Application.Mappers;

[Mapper]
public static partial class MessageMapper
{
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

    [MapperIgnoreSource(nameof(Message.Chat))]
    [MapperIgnoreSource(nameof(Message.ChatId))]
    [MapperIgnoreSource(nameof(Message.Sender))]
    [MapProperty(nameof(Message.Text), nameof(MessageDto.Content))]
    [MapProperty(nameof(Message.CreatedAt), nameof(MessageDto.SentAt))]
    [MapProperty(nameof(Message.Sentiment), nameof(MessageDto.Sentiment))]
    public static partial MessageDto ToDto(this Message message);

    public static partial IEnumerable<MessageDto> ToDtoList(
        this IEnumerable<Message> messages);

    private static string MapSentimentToString(
        SentimentType sentiment) => sentiment.ToString();
}
