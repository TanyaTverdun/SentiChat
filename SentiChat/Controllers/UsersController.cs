using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentiChat.Application.DTOs.Users;
using SentiChat.Application.Interfaces.Security;
using SentiChat.Application.Interfaces;

namespace SentiChat.Controllers;

/// <summary>
/// Handles user profile operations.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IValidator<UpdateProfileRequestDto> _updateProfileValidator;

    public UsersController(
        IUserService userService,
        ICurrentUserService currentUserService,
        IValidator<UpdateProfileRequestDto> updateProfileValidator)
    {
        _userService = userService;
        _currentUserService = currentUserService;
        _updateProfileValidator = updateProfileValidator;
    }

    /// <summary>
    /// Retrieves the profile information of the currently authenticated user.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A <see cref="UserProfileDto"/> containing the user's details.
    /// </returns>
    /// <response code="200">
    /// Successfully retrieved the user profile.
    /// </response>
    /// <response code="401">
    /// User is not authenticated.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred.
    /// </response>
    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserProfileDto>> GetMyProfile(
        CancellationToken cancellationToken)
    {
        var userId = _currentUserService.GetUserId();
        var profile = await _userService.GetUserProfileAsync(
            userId, 
            cancellationToken);

        return Ok(profile);
    }

    /// <summary>
    /// Updates the profile information of the currently authenticated user.
    /// </summary>
    /// <param name="request">
    /// The data transfer object containing the 
    /// new profile details (name, email, bio).
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// A <see cref="UserProfileDto"/> containing the updated user profile.
    /// </returns>
    /// <response code="200">
    /// Successfully updated the user profile.
    /// </response>
    /// <response code="400">
    /// Validation failed for the requested update data 
    /// (e.g., invalid email format or email already in use).
    /// </response>
    /// <response code="401">
    /// User is not authenticated.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred.
    /// </response>
    [HttpPut("profile")]
    [ProducesResponseType(typeof(UserProfileDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<UserProfileDto>> UpdateMyProfile(
        [FromBody] UpdateProfileRequestDto request,
        CancellationToken cancellationToken)
    {
        var validationResult = await _updateProfileValidator
            .ValidateAsync(
                request, 
                cancellationToken);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var userId = _currentUserService.GetUserId();
        var updatedProfile = await _userService
            .UpdateUserProfileAsync(
                userId, 
                request, 
                cancellationToken);

        return Ok(updatedProfile);
    }
}