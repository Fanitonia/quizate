using FluentValidation;
using Quizate.API.Contracts;

namespace Quizate.API.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UsernameOrEmail)
            .NotEmpty()
                .WithMessage("Username or email is required.")

            .EmailAddress()
                .When(x => !string.IsNullOrWhiteSpace(x.UsernameOrEmail) && x.UsernameOrEmail.Contains('@'),
                    ApplyConditionTo.CurrentValidator)
                .WithMessage("Invalid email format.")

            .MaximumLength(255)
                .When(x => !string.IsNullOrWhiteSpace(x.UsernameOrEmail) && x.UsernameOrEmail.Contains('@'),
                    ApplyConditionTo.CurrentValidator)
                .WithMessage("Email must not exceed 255 characters.")

            .MaximumLength(25)
                .When(x => !string.IsNullOrWhiteSpace(x.UsernameOrEmail) && !x.UsernameOrEmail.Contains('@'),
                    ApplyConditionTo.CurrentValidator)
                .WithMessage("Username must not exceed 25 characters.");

        RuleFor(x => x.Password)
            .NotEmpty()
                .WithMessage("Password is required")
            .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long")
            .MaximumLength(256)
                .WithMessage("Password must not exceed 256 characters.")
            .Matches("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{1,}$")
                .WithMessage("Password must contain at least one uppercase letter, one lowercase letter and one number.");
    }
}
