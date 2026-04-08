using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentiChat.Application.DTOs.Chats;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Application.Mappers;

namespace SentiChat.Controllers;

/// <summary>
/// Manages user chats, including creating personal chats and retrieving chat history.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<CreatePersonalChatRequestDto> _createChatValidator;

    public ChatsController(
        IChatService chatService,
        ICurrentUserService currentUserService,
        IValidator<CreatePersonalChatRequestDto> createChatValidator)
    {
        this._chatService = chatService;
        this._currentUserService = currentUserService;
        this._createChatValidator = createChatValidator;
    }

    /// <summary>
    /// Retrieves a list of chats for the currently authenticated user.
    /// </summary>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A collection of chat summary data.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ChatListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ChatListItemDto>>> GetChats(
        CancellationToken cancellationToken)
    {
        var userId = this._currentUserService.GetUserId();

        var chats = await this._chatService
            .GetUserChatsAsync(
                userId, 
                cancellationToken);

        return Ok(chats);
    }

    /// <summary>
    /// Retrieves an existing personal chat or creates a new one with the specified target user.
    /// </summary>
    /// <param name="request">The data containing the target user's ID.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>The unique identifier of the chat.</returns>
    [HttpPost("personal")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<PersonalChatCreatedResponseDto>> GetOrCreatePersonalChat(
        [FromBody] CreatePersonalChatRequestDto request,
        CancellationToken cancellationToken)
    {
        var validationResult = await this._createChatValidator.ValidateAsync(
            request,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var currentUserId = this._currentUserService.GetUserId();

        var chatId = await this._chatService.GetOrCreatePersonalChatAsync(
            currentUserId,
            request.TargetUserEmail,
            cancellationToken);

        return Ok(chatId.MapToCreatedResponseDto());
    }
}
