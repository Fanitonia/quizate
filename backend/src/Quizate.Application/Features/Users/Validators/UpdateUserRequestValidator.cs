using FluentValidation;
using Quizate.Application.Features.Users.DTOs.Requests;

namespace Quizate.Application.Features.Users.Validators;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(r => r.Username)
            .MinimumLength(3)
                .WithMessage("Username must be 3-20 characters long.")
            .MaximumLength(20)
                .WithMessage("Username must be 3-20 characters long.")
            .Matches("^[a-z0-9_]+$")
                .WithMessage("Username can only contain lowercase letters, numbers, and underscores.");

        RuleFor(r => r.DisplayName)
            .MinimumLength(3)
                .WithMessage("Display name must be 3-25 characters long.")
            .MaximumLength(25)
                .WithMessage("Display name must be 3-25 characters long.")
            .Matches("^[a-zA-Z0-9_ ]+$")
                .WithMessage("Display name can only contain letters, numbers, spaces, and underscores.");

        RuleFor(r => r.Email)
            .EmailAddress()
            .WithMessage("Invalid email address format.");
    }
}
