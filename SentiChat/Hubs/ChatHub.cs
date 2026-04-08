using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SentiChat.Application.Constants;
using SentiChat.Domain.Interfaces.Repositories;

namespace SentiChat.Hubs;

/// <summary>
/// A hub for connecting clients (frontend) to WebSocket.
/// </summary>
[Authorize]
public class ChatHub : Hub
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChatHub(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = Guid.Parse(Context.UserIdentifier!);

        var user = await _userRepository.GetByIdAsync(
            userId, 
            Context.ConnectionAborted);

        if (user != null)
        {
            user.IsOnline = true;
            await _unitOfWork.SaveChangesAsync(Context.ConnectionAborted);
        }

        await Clients.All.SendAsync(
            SignalRConstants.UserStatusChanged, 
            userId, true, 
            DateTime.UtcNow);

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = Guid.Parse(Context.UserIdentifier!);
        var now = DateTime.UtcNow;

        var user = await _userRepository.GetByIdAsync(
            userId, 
            Context.ConnectionAborted);

        if (user != null)
        {
            user.IsOnline = false;
            user.LastSeen = now;
            await _unitOfWork.SaveChangesAsync(Context.ConnectionAborted);
        }

        await Clients.All.SendAsync(
            SignalRConstants.UserStatusChanged, 
            userId, 
            false, 
            now);
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendTypingNotification(
        Guid chatId, 
        string receiverId, 
        bool isTyping)
    {
        var senderId = Context.UserIdentifier;
        await Clients
            .User(receiverId)
            .SendAsync(
                SignalRConstants.UserTyping, 
                senderId, 
                chatId, 
                isTyping);
    }
}
