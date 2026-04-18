using FluentValidation;
using Quizate.Application.Features.Auth.DTOs.Requests;

namespace Quizate.Application.Features.Auth.Validators;


public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
                .WithMessage("Username is required.")
            .MaximumLength(20)
                .WithMessage("Username must not exceed 20 characters.")
            .Matches("^[a-z0-9_]+$")
                .WithMessage("Username can only contain lowercase letters, numbers, and underscores.");

        RuleFor(x => x.Email)
            .EmailAddress()
                .WithMessage("Invalid email format.");

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
