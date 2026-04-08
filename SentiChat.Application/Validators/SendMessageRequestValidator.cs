using FluentValidation;
using SentiChat.Application.DTOs.Messages;

namespace SentiChat.Application.Validators;

public class SendMessageRequestValidator : AbstractValidator<SendMessageRequestDto>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x.Content)
             .NotEmpty()
             .WithMessage("Message content cannot be empty.")
             .MaximumLength(2000)
             .WithMessage("Message is too long (maximum 2000 characters).");
    }
}
