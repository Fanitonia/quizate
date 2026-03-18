using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Common.Errors;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Requests;
using Quizate.Application.Features.Users.Errors;
using Quizate.Application.Features.Users.Interfaces;
using Quizate.Domain.Entities.Users;
using Quizate.Persistence;

namespace Quizate.Application.Features.Users.Services;

public class UserCommandService(
    QuizateDbContext context,
    IPasswordHasher<User> passwordHasher) : IUserCommandService
{
    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await context.Users
            .FindAsync(userId);

        if (user == null)
            return CommonErrors.NotFound("User", userId.ToString());

        user.MarkAsDeleted();
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateUserInfoAsync(UpdateUserRequest request, Guid userId)
    {
        var user = await context.Users
            .FindAsync(userId);

        if (user == null)
            return CommonErrors.NotFound("User", userId.ToString());

        if (request.Username != null)
        {
            var isUsernameTaken = await context.Users
                .AnyAsync(u => u.Username == request.Username && u.Id != userId);

            if (isUsernameTaken)
                return UserErrors.UsernameTaken(request.Username);

            user.UpdateUsername(request.Username);
        }

        if (request.Email != null)
        {
            var isEmailTaken = await context.Users
                .AnyAsync(u => u.Email == request.Email && u.Id != userId);

            if (isEmailTaken)
                return UserErrors.EmailTaken(request.Email);

            user.UpdateEmail(request.Email);
        }

        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(PasswordChangeRequest request, Guid userId)
    {
        var user = await context.Users.FindAsync(userId);

        if (user == null)
            return CommonErrors.NotFound("User", userId.ToString());

        var passwordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.OldPassword);

        if (passwordResult == PasswordVerificationResult.Failed)
            return UserErrors.IncorrectPassword;

        var newPasswordHash = passwordHasher.HashPassword(user, request.NewPassword);
        user.UpdatePasswordHash(newPasswordHash);

        context.SaveChanges();

        return Result.Success();
    }
}