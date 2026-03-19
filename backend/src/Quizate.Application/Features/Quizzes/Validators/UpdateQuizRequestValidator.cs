using FluentValidation;
using Quizate.Application.Features.Quizzes.DTOs.Requests;

namespace Quizate.Application.Features.Quizzes.Validators;

public class UpdateQuizRequestValidator : AbstractValidator<UpdateQuizRequest>
{
    public UpdateQuizRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty()
                .WithMessage("Quiz title is required.")
            .MaximumLength(200)
                .WithMessage("Quiz title must not exceed 200 characters.")
            .When(request => request.Title != null);


        RuleFor(request => request.Description)
            .MaximumLength(1000)
                .WithMessage("Quiz description must not exceed 1000 characters.")
            .When(request => request.Description != null);
    }
}
