using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SentiChat.Application.DTOs.Users;
using SentiChat.Application.Interfaces;

namespace SentiChat.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IValidator<RegisterUserDto> _validator;

    public AuthController(
        IUserService userService,
        IValidator<RegisterUserDto> validator)
    {
        this._userService = userService;
        this._validator = validator;
    }

    /// <summary>
    /// Registers a new user in the system.
    /// </summary>
    /// <param name="registerDto">User registration details including email and password.</param>
    /// <returns>Returns the unique identifier (GUID) of the newly created user.</returns>
    /// <response code="200">User successfully registered.</response>
    /// <response code="400">Validation failed or user with this email already exists.</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterUserDto registerUserDto,
        CancellationToken cancellationToken)
    {
        var validationResult = await this._validator.ValidateAsync(
            registerUserDto,
            cancellationToken);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var userId = await this._userService.RegisterUserAsync(
            registerUserDto,
            cancellationToken);

        return Ok(new
        {
            UserId = userId
        });
    }
}
