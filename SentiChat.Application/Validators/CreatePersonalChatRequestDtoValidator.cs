using FluentValidation;
using SentiChat.Application.DTOs.Chats;

namespace SentiChat.Application.Validators;

/// <summary>
/// Validates the request data for creating a new personal (1-on-1) chat.
/// </summary>
public class CreatePersonalChatRequestDtoValidator 
    : AbstractValidator<CreatePersonalChatRequestDto>
{
    /// <summary>
    /// Validates the request data for creating a new personal (1-on-1) chat.
    /// </summary>
    public CreatePersonalChatRequestDtoValidator()
    {
        RuleFor(x => x.TargetUserEmail)
            .NotEmpty()
            .WithMessage("Target user email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");
    }
}
