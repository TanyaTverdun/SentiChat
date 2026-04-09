using FluentValidation;
using SentiChat.Application.DTOs.Users;

namespace SentiChat.Application.Validators;

/// <summary>
/// Validates the request data for user login.
/// </summary>
public class LoginUserDtoValidator : AbstractValidator<LoginUserDto>
{
    /// <summary>
    /// Validates the request data for user login.
    /// </summary>
    public LoginUserDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.");
    }
}
