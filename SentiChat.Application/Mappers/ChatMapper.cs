using Riok.Mapperly.Abstractions;
using SentiChat.Application.DTOs.Chats;
using SentiChat.Domain.Entities;

namespace SentiChat.Application.Mappers;

/// <summary>
/// Provides mapping methods for Chat-related 
/// entities using Riok.Mapperly.
/// </summary>
[Mapper]
public static partial class ChatMapper
{
    /// <summary>
    /// Maps a <see cref="Chat"/> entity to a 
    /// <see cref="ChatListItemDto"/> for the sidebar list.
    /// Custom parameters are required because display details 
    /// depend on the chat type (Group vs. Personal).
    /// </summary>
    /// <param name="chat">
    /// The source chat entity.
    /// </param>
    /// <param name="name">
    /// The resolved display name (either the group title or the other user's name).
    /// </param>
    /// <param name="initials">
    /// The calculated initials for the chat avatar.
    /// </param>
    /// <param name="lastMessageText">
    /// The content of the most recent message, if any.
    /// </param>
    /// <param name="lastMessageTime">
    /// The timestamp of the most recent message, if any.
    /// </param>
    /// <param name="partnerId">
    /// The unique identifier of the other participant (for personal chats).
    /// </param>
    /// <param name="isOnline">
    /// The current online status of the partner.
    /// </param>
    /// <returns>
    /// A fully populated <see cref="ChatListItemDto"/>.
    /// </returns>
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
        DateTime? lastMessageTime,
        Guid partnerId,
        bool isOnline);

    /// <summary>
    /// Wraps a newly generated Chat ID into a standard response DTO.
    /// </summary>
    /// <param name="chatId">
    /// The unique identifier of the newly created chat.
    /// </param>
    /// <returns>
    /// A <see cref="PersonalChatCreatedResponseDto"/>.
    /// </returns>
    public static PersonalChatCreatedResponseDto MapToCreatedResponseDto(this Guid chatId)
    {
        return new PersonalChatCreatedResponseDto 
        { 
            ChatId = chatId 
        };
    }
}
