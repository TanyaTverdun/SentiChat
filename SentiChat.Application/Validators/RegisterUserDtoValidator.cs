using FluentValidation;
using SentiChat.Application.DTOs.Users;

namespace SentiChat.Application.Validators;

/// <summary>
/// Validates the request data for registering a new user.
/// </summary>
public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    /// <summary>
    /// Validates the request data for registering a new user.
    /// </summary>
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .MaximumLength(255)
            .WithMessage("Email cannot exceed 255 characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.")
            .Matches(@"[A-Z]")
            .WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]")
            .WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]")
            .WithMessage("Password must contain at least one digit.")
            .Matches(@"[\W]")
            .WithMessage("Password must contain at least one special character.");
    }
}
