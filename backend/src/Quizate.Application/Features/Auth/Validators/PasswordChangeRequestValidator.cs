using FluentValidation;
using Quizate.Application.Features.Auth.DTOs.Requests;

namespace Quizate.Application.Features.Auth.Validators;

public class PasswordChangeRequestValidator : AbstractValidator<PasswordChangeRequest>
{
    public PasswordChangeRequestValidator()
    {
        RuleFor(x => x.NewPassword)
           .NotEmpty()
               .WithMessage("New password is required")
           .MinimumLength(8)
               .WithMessage("New password must be at least 8 characters long")
           .MaximumLength(256)
               .WithMessage("New password must not exceed 256 characters.")
           .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{1,}$")
               .WithMessage("New password must contain at least one uppercase letter, one lowercase letter and one number.");

        RuleFor(x => x.OldPassword)
            .NotEmpty()
                .WithMessage("Old password is required.");
    }
}
