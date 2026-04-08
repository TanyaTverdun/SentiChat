using SentiChat.Application.DTOs.Messages;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Interfaces.AzureAI;
using SentiChat.Application.Mappers;
using SentiChat.Domain.Entities;
using SentiChat.Domain.Interfaces.Repositories;

namespace SentiChat.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly ISentimentAnalysisService _sentimentAnalysisService;
    private readonly IUnitOfWork _unitOfWork;

    public MessageService(
        IMessageRepository messageRepository,
        IChatRepository chatRepository,
        ISentimentAnalysisService sentimentAnalysisService, 
        IUnitOfWork unitOfWork)
    {
        this._messageRepository = messageRepository;
        this._chatRepository = chatRepository;
        this._sentimentAnalysisService = sentimentAnalysisService;
        this._unitOfWork = unitOfWork;
    }

    public async Task<MessageDto> SendMessageAsync(
        Guid chatId, 
        Guid senderId, 
        string content, 
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            throw new ArgumentException("Message content cannot be empty.");
        }

        var chat = await this._chatRepository.GetChatByIdAsync(chatId, cancellationToken);
        if (chat == null)
        {
            throw new KeyNotFoundException($"Chat with ID {chatId} was not found.");
        }

        if (!chat.Members.Any(m => m.UserId == senderId))
        {
            throw new KeyNotFoundException($"Sender is not a member of this chat.");
        }

        var sentiment = await this._sentimentAnalysisService
            .AnalyzeSentimentAsync(content);

        var messageId = Guid.NewGuid();
        var sentAt = DateTime.UtcNow;

        var message = MessageMapper.ToEntity(
            messageId,
            chatId,
            senderId,
            content,
            sentAt,
            sentiment);

        await _messageRepository.AddAsync(
            message, 
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return message.ToDto();
    }

    public async Task<IEnumerable<MessageDto>> GetChatHistoryAsync(
        Guid chatId,
        int pageSize,
        DateTime? before,
        CancellationToken cancellationToken)
    {
        var chat = await this._chatRepository.GetChatByIdAsync(chatId, cancellationToken);
        if (chat == null)
        {
            throw new KeyNotFoundException($"Chat with ID {chatId} was not found.");
        }

        var messages = await this._messageRepository.GetMessagesAsync(
            chatId,
            pageSize,
            before,
            cancellationToken);

        return messages.ToDtoList();
    }
}
