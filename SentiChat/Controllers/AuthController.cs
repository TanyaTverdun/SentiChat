using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SentiChat.Application.DTOs.Users;
using SentiChat.Application.Interfaces;
using SentiChat.Application.Mappers;

namespace SentiChat.Controllers;

/// <summary>
/// Handles user authentication, including registration and login processes.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<RegisterUserDto> _registerValidator;
    private readonly IValidator<LoginUserDto> _loginValidator;

    public AuthController(
        IUserService userService,
        IValidator<RegisterUserDto> registerValidator,
        IValidator<LoginUserDto> loginValidator)
    {
        this._userService = userService;
        this._registerValidator = registerValidator;
        this._loginValidator = loginValidator;
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="registerUserDto">
    /// User registration details including email and password.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// Returns an authentication response containing the JWT token.
    /// </returns>
    /// <response code="200">
    /// User successfully registered.
    /// </response>
    /// <response code="400">
    /// Validation failed or user with this email already exists.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred.
    /// </response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponseDto>> RegisterAsync(
        [FromBody] RegisterUserDto registerUserDto,
        CancellationToken cancellationToken)
    {
        var validationResult = await this._registerValidator.ValidateAsync(
            registerUserDto,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var token = await this._userService.RegisterUserAsync(
            registerUserDto,
            cancellationToken);

        return Ok(token.MapToAuthResponseDto());
    }

    /// <summary>
    /// Authenticates a user and returns their unique identifier.
    /// </summary>
    /// <param name="loginUserDto">
    /// The login credentials (email and password).
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <returns>
    /// Returns an authentication response containing the JWT token.
    /// </returns>
    /// <response code="200">
    /// User successfully authenticated.
    /// </response>
    /// <response code="400">
    /// Validation failed (e.g., empty email or password).
    /// </response>
    /// <response code="401">
    /// Invalid email or password.
    /// </response>
    /// <response code="500">
    /// An unexpected server error occurred.
    /// </response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthResponseDto>> LoginAsync(
        [FromBody] LoginUserDto loginUserDto, 
        CancellationToken cancellationToken)
    {
        var validationResult = await this._loginValidator.ValidateAsync(
            loginUserDto,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var token = await this._userService.LoginUserAsync(
            loginUserDto,
            cancellationToken);

        return Ok(token.MapToAuthResponseDto());
    }
}
