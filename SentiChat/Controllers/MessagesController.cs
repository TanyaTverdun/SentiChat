using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SentiChat.Application.DTOs.Messages;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Application.Constants;
using SentiChat.Hubs;

namespace SentiChat.Controllers;

/// <summary>
/// Handles sending messages and retrieving chat history.
/// </summary>
[Authorize]
[ApiController]
[Route("api/chats/{chatId}/messages")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<SendMessageRequestDto> _validator;
    private readonly IHubContext<ChatHub> _hubContext;

    public MessagesController(
        IMessageService messageService,
        ICurrentUserService currentUserService,
        IValidator<SendMessageRequestDto> validator,
        IHubContext<ChatHub> hubContext)
    {
        this._messageService = messageService;
        this._currentUserService = currentUserService;
        this._validator = validator;
        this._hubContext = hubContext;
    }

    /// <summary>
    /// Sends a new message, triggers sentiment analysis, 
    /// and broadcasts it in real-time to other chat members.
    /// </summary>
    /// <param name="chatId">
    /// The unique identifier of the chat.
    /// </param>
    /// <param name="request">
    /// The message content.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// The created message with sentiment analysis results.
    /// </returns>
    /// <response code="200">
    /// Message successfully sent and broadcasted to other members.
    /// </response>
    /// <response code="400">
    /// Validation failed (e.g., empty message content).
    /// </response>
    /// <response code="401">
    /// User is not authenticated.
    /// </response>
    /// <response code="404">
    /// Chat not found or user is not a member of this chat.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred.
    /// </response>
    [HttpPost]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MessageDto>> SendMessageAsync(
        [FromRoute] Guid chatId,
        [FromBody] SendMessageRequestDto request,
        CancellationToken cancellationToken)
    {
        var validationResult = await this._validator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var senderId = this._currentUserService.GetUserId();

        var (message, receiverIds) = await _messageService.SendMessageAsync(
            chatId,
            senderId,
            request.Content,
            cancellationToken);

        if (receiverIds.Any())
        {
            await _hubContext.Clients.Users(receiverIds)
                .SendAsync(
                    SignalRConstants.ReceiveMessage, 
                    message, 
                    cancellationToken);
        }

        return Ok(message);
    }

    /// <summary>
    /// Retrieves the message history for a specific chat.
    /// </summary>
    /// <param name="chatId">
    /// The unique identifier of the chat.
    /// </param>
    /// <param name="pageSize">
    /// The number of messages to retrieve (default is 50).
    /// </param>
    /// <param name="before">
    /// Optional timestamp to load older messages.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A list of messages for the chat.
    /// </returns>
    /// <response code="200">
    /// Successfully retrieved the chat history.
    /// </response>
    /// <response code="401">
    /// User is not authenticated.
    /// </response>
    /// <response code="404">
    /// Chat not found or user is not a member of this chat.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred.
    /// </response>
    [HttpGet]
    [ProducesResponseType(
        typeof(IEnumerable<MessageDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<MessageDto>>>
        GetChatHistoryAsync(
            CancellationToken cancellationToken,
            [FromRoute] Guid chatId,
            [FromQuery] int pageSize = 50,
            [FromQuery] DateTime? before = null)
    {
        var messages = await this._messageService.GetChatHistoryAsync(
            chatId,
            pageSize,
            before,
            cancellationToken);

        return Ok(messages);
    }
}
