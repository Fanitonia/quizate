using FluentValidation;

namespace Quizate.Application.Common.Pagination.Validators;

public class PaginationParametersValidators : AbstractValidator<PaginationParameters>
{
    public PaginationParametersValidators()
    {
        RuleFor(p => p.PageSize)
            .GreaterThan(0)
                .WithMessage("Page size must be greater than 0.")
            .LessThanOrEqualTo(50)
                .WithMessage("Page size must not exceed 50.");

        RuleFor(p => p.PageNumber)
            .GreaterThan(0)
                .WithMessage("Page number must be greater than 0.")
            .LessThanOrEqualTo(42949672)
                .WithMessage("Page number is too big.");
    }
}
