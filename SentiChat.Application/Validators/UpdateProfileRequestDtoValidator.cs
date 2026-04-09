using FluentValidation;
using SentiChat.Application.DTOs.Users;

namespace SentiChat.Application.Validators;

/// <summary>
/// Validates the request data for updating a user profile.
/// </summary>
public class UpdateProfileRequestDtoValidator : AbstractValidator<UpdateProfileRequestDto>
{
    /// <summary>
    /// Validates the request data for updating a user profile.
    /// </summary>
    public UpdateProfileRequestDtoValidator()
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

        RuleFor(x => x.Bio)
            .MaximumLength(500)
            .WithMessage("Bio cannot exceed 500 characters.");
    }
}
