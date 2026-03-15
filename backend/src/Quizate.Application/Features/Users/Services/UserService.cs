using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Features.Users.Interfaces;
using Quizate.Application.Shared.Result;
using Quizate.Domain.Entities.Users;
using Quizate.Persistence;

namespace Quizate.Application.Features.Users.Services;

public class UserService(
    QuizateDbContext context,
    IMapper mapper,
    IPasswordHasher<User> passwordHasher) : IUserService
{
    public async Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result<UserInfoResponse>.Failure("User not found.");

        return Result<UserInfoResponse>.Success(mapper.Map<UserInfoResponse>(user));
    }

    public async Task<Result<DetailedUserInfoResponse>> GetDetailedUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result<DetailedUserInfoResponse>.Failure("User not found.");

        return Result<DetailedUserInfoResponse>.Success(mapper.Map<DetailedUserInfoResponse>(user));
    }

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

    public async Task<Result> UpdateUserAsync(Guid userId, UpdateUserRequest request)
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