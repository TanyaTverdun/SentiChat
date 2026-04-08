using FluentValidation;
using SentiChat.Application.DTOs.Chats;

namespace SentiChat.Application.Validators;

public class CreatePersonalChatRequestDtoValidator 
    : AbstractValidator<CreatePersonalChatRequestDto>
{
    public CreatePersonalChatRequestDtoValidator()
    {
        RuleFor(x => x.TargetUserEmail)
            .NotEmpty()
            .WithMessage("Target user email is required.")
            .EmailAddress()
            .WithMessage("A valid email address is required.");
    }
}
