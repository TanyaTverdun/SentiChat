using SentiChat.Application.DTOs.Chats;
using SentiChat.Application.Interfaces;
using SentiChat.Domain.Interfaces.Repositories;
using SentiChat.Application.Constants;
using SentiChat.Application.Mappers;
using SentiChat.Application.Extensions;
using SentiChat.Domain.Entities;

namespace SentiChat.Application.Services;

/// <summary>
/// Provides the standard implementation of <see cref="IChatService"/>.
/// </summary>
public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChatService(
        IChatRepository chatRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        this._chatRepository = chatRepository;
        this._userRepository = userRepository;
        this._unitOfWork = unitOfWork;
    }

    /// <inheritdoc />
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
            Guid partnerId = Guid.Empty;
            bool isOnline = false;

            if (!chat.IsGroup)
            {
                var otherMember = chat.Members
                    .FirstOrDefault(m => m.UserId != userId);

                if (otherMember != null)
                {
                    displayTitle = otherMember.User.Name;
                    partnerId = otherMember.UserId;
                    isOnline = otherMember.User.IsOnline;
                }
            }

            var lastMessage = chat.Messages
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault();

            var dto = chat.MapToListItemDto(
                displayTitle,
                displayTitle.ToInitials(),
                lastMessage?.Text,
                lastMessage?.CreatedAt,
                partnerId,
                isOnline);

            chatList.Add(dto);
        }

        return chatList
            .OrderByDescending(c => c.LastMessageTime ?? DateTime.MinValue)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<Guid> GetOrCreatePersonalChatAsync(
        Guid currentUserId,
        string userEmail,
        CancellationToken cancellationToken)
    {
        var targetUser = await this._userRepository
            .GetByEmailAsync(
                userEmail,
                cancellationToken);

        if (targetUser == null)
        {
            throw new ArgumentException($"User with email '{userEmail}' does not exist.");
        }

        var targetUserId = targetUser.Id;

        if (currentUserId == targetUserId)
        {
            throw new ArgumentException("You cannot create a personal chat with yourself.");
        }

        var existingChat = await this._chatRepository
            .GetPersonalChatBetweenUsersAsync(
                currentUserId,
                targetUserId,
                cancellationToken);

        if (existingChat != null)
        {
            return existingChat.Id;
        }

        var newChat = Chat.CreatePersonal(currentUserId, targetUserId);

        await this._chatRepository.AddAsync(
            newChat,
            cancellationToken);

        await this._unitOfWork.SaveChangesAsync(cancellationToken);

        return newChat.Id;
    }
}
