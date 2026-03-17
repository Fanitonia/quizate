using FluentValidation;
using Quizate.Application.Features.Topics.DTOs.Requests;

namespace Quizate.Application.Features.Topics.Validators;

public class UpdateTopicRequestValidator : AbstractValidator<UpdateTopicRequest>
{
    public UpdateTopicRequestValidator()
    {
        RuleFor(t => t.DisplayName)
            .Matches("^[A-Za-z0-9 -]+$")
                .WithMessage("Display name can only contain letters, numbers, spaces and hyphens.")
            .MaximumLength(50)
                .WithMessage("Display name must not exceed 50 characters.");

        RuleFor(t => t.Description)
            .MaximumLength(500)
                .WithMessage("Description must not exceed 500 characters.");
    }
}
