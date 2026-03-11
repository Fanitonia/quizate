using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.Application.Features.Users.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Features.Users.Interfaces;
using Quizate.Application.Shared.Result;
using Quizate.Persistence;

namespace Quizate.Application.Features.Users.Services;

public class UserService(
    QuizateDbContext context,
    IMapper mapper) : IUserService
{
    public async Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result<UserInfoResponse>.Failure("User not found.");

        return Result<UserInfoResponse>.Success(mapper.Map<UserInfoResponse>(user));
    }

    public async Task<Result<MyInfoResponse>> GetMyInfoAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result<MyInfoResponse>.Failure("User not found.");

        return Result<MyInfoResponse>.Success(mapper.Map<MyInfoResponse>(user));
    }

    public async Task<Result> DeleteUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result.Failure("User not found.");

        user.MarkAsDeleted();

        await context.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> UpdateUserAsync(Guid userId, UpdateMyInfoRequest request, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result.Failure("User not found.");

        if (request.Username != null)
        {
            var isUsernameTaken = await context.Users
                .AnyAsync(u => u.Username == request.Username && u.Id != userId, ct);

            if (isUsernameTaken)
                return Result.Failure("Username is already taken.");

            user.UpdateUsername(request.Username);
        }

        if (request.Email != null)
        {
            var isEmailTaken = await context.Users
                .AnyAsync(u => u.Email == request.Email && u.Id != userId, ct);

            if (isEmailTaken)
                return Result.Failure("Email is already taken.");

            user.UpdateEmail(request.Email);
        }

        await context.SaveChangesAsync(ct);

        return Result.Success();
    }
}