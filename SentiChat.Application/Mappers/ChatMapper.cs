using Riok.Mapperly.Abstractions;
using SentiChat.Application.DTOs.Chats;
using SentiChat.Domain.Entities;

namespace SentiChat.Application.Mappers;

[Mapper]
public static partial class ChatMapper
{
    [MapProperty(nameof(Chat.Id), nameof(ChatListItemDto.ChatId))]
    [MapperIgnoreSource(nameof(Chat.Title))]
    [MapperIgnoreSource(nameof(Chat.IsGroup))]
    [MapperIgnoreSource(nameof(Chat.Messages))]
    [MapperIgnoreSource(nameof(Chat.Members))]
    public static partial ChatListItemDto MapToListItemDto(
        this Chat chat,
        string name,
        string initials,
        string? lastMessageText,
        DateTime? lastMessageTime);
}
