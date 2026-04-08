using SentiChat.Application.DTOs.Chats;
using SentiChat.Application.Interfaces;
using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Application.Constants;
using SentiChat.Application.Mappers;
using SentiChat.Application.Extensions;
using SentiChat.Domain.Entities;

namespace SentiChat.Application.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(
        IChatRepository chatRepository,
        IUnitOfWork unitOfWork)
    {
        this._chatRepository = chatRepository;
        this._unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ChatListItemDto>> GetUserChatsAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        var chats = await this._chatRepository
            .GetUserChatsAsync(
            userId,
            cancellationToken);

        var chatList = new List<ChatListItemDto>();

        foreach (var chat in chats)
        {
            string displayTitle = chat.Title ?? ChatConstants.DefaultChatName;

            if (!chat.IsGroup)
            {
                var otherMember = chat.Members
                    .FirstOrDefault(m => m.UserId != userId);

                if (otherMember != null)
                {
                    displayTitle = otherMember.User.Name;
                }
            }

            var lastMessage = chat.Messages
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault();

            var dto = chat.MapToListItemDto(
                displayTitle,
                displayTitle.ToInitials(),
                lastMessage?.Text,
                lastMessage?.CreatedAt);

            chatList.Add(dto);
        }

        return chatList
            .OrderByDescending(c => c.LastMessageTime ?? DateTime.MinValue)
            .ToList();
    }

    public async Task<Guid> GetOrCreatePersonalChatAsync(
        Guid user1Id,
        Guid user2Id,
        CancellationToken cancellationToken)
    {
        var existingChat = await this._chatRepository
            .GetPersonalChatBetweenUsersAsync(
                user1Id,
                user2Id,
                cancellationToken);

        if (existingChat != null)
        {
            return existingChat.Id;
        }

        var newChat = Chat.CreatePersonal(user1Id, user2Id);

        await this._chatRepository.AddAsync(
            newChat,
            cancellationToken);

        await this._unitOfWork.SaveChangesAsync(cancellationToken);

        return newChat.Id;
    }
}
