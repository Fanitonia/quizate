using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Requests;
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
            return Result.Failure("User not found.");

        user.MarkAsDeleted();
        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> UpdateUserInfoAsync(UpdateUserRequest request, Guid userId)
    {
        var user = await context.Users
            .FindAsync(userId);

        if (user == null)
            return Result.Failure("User not found.");

        if (request.Username != null)
        {
            var isUsernameTaken = await context.Users
                .AnyAsync(u => u.Username == request.Username && u.Id != userId);

            if (isUsernameTaken)
                return Result.Failure("Username is already taken.");

            user.UpdateUsername(request.Username);
        }

        if (request.Email != null)
        {
            var isEmailTaken = await context.Users
                .AnyAsync(u => u.Email == request.Email && u.Id != userId);

            if (isEmailTaken)
                return Result.Failure("Email is already taken.");

            user.UpdateEmail(request.Email);
        }

        await context.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result> ChangePasswordAsync(PasswordChangeRequest request, Guid userId)
    {
        var user = await context.Users.FindAsync(userId);

        if (user == null)
            return Result.Failure("Could not found the user.");

        var passwordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.OldPassword);

        if (passwordResult == PasswordVerificationResult.Failed)
            return Result.Failure("Password is incorrect.");

        var newPasswordHash = passwordHasher.HashPassword(user, request.NewPassword);
        user.UpdatePasswordHash(newPasswordHash);

        context.SaveChanges();

        return Result.Success();
    }
}