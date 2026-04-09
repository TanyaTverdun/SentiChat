using FluentValidation;
using SentiChat.Application.DTOs.Messages;

namespace SentiChat.Application.Validators;

/// <summary>
/// Validates the request data for sending a new message in a chat.
/// </summary>
public class SendMessageRequestValidator 
    : AbstractValidator<SendMessageRequestDto>
{
    /// <summary>
    /// Validates the request data for sending a new message in a chat.
    /// </summary>
    public SendMessageRequestValidator()
    {
        RuleFor(x => x.Content)
             .NotEmpty()
             .WithMessage("Message content cannot be empty.")
             .MaximumLength(2000)
             .WithMessage("Message is too long (maximum 2000 characters).");
    }
}
