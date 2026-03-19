using FluentValidation;
using Quizate.Application.Features.Quizzes.DTOs.Requests;

namespace Quizate.Application.Features.Quizzes.Validators;

public class CreateQuizRequestValidator : AbstractValidator<CreateQuizRequest>
{
    public CreateQuizRequestValidator()
    {
        RuleFor(q => q.Title)
            .NotEmpty()
                .WithMessage("Quiz title is required.")
            .MaximumLength(100)
                .WithMessage("Quiz title must not exceed 100 characters.");

        RuleFor(q => q.Description)
            .MaximumLength(400)
                .WithMessage("Quiz description must not exceed 400 characters.");

        RuleFor(q => q.Questions)
            .NotEmpty()
                .WithMessage("A quiz must have at least one question.");

        RuleFor(q => q.LanguageCode)
            .NotEmpty()
                .WithMessage("Language code is required.");
    }
}
