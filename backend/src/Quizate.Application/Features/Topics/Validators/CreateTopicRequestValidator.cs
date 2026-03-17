using FluentValidation;
using Quizate.Application.Features.Topics.DTOs.Requests;

namespace Quizate.Application.Features.Topics.Validators;

public class CreateTopicRequestValidator : AbstractValidator<CreateTopicRequest>
{
    public CreateTopicRequestValidator()
    {
        RuleFor(t => t.Name)
            .NotEmpty()
                .WithMessage("Name is required.")
            .Matches("^[A-Za-z-]+$")
                .WithMessage("Name can only contain letters and hyphens.");

        RuleFor(t => t.Description)
            .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");

        RuleFor(t => t.DisplayName)
            .NotEmpty()
                .WithMessage("Display name is required.")
            .Matches("^[A-Za-z0-9 -]+$")
                .WithMessage("Display name can only contain letters, numbers, spaces and hyphens.")
            .MaximumLength(50)
                .WithMessage("Display name must not exceed 50 characters.");
    }
}
