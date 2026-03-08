using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Data;
using Quizate.API.Shared.Result;

namespace Quizate.API.Services.Users;

public class UserService(
    QuizateDbContext context,
    IMapper mapper) : IUserService
{
    public async Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result<UserInfoResponse>.Fail(new[] { "User not found." });

        return Result<UserInfoResponse>.Ok(mapper.Map<UserInfoResponse>(user));
    }

    public async Task<Result<MyInfoResponse>> GetMyInfoAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result<MyInfoResponse>.Fail(new[] { "User not found." });

        return Result<MyInfoResponse>.Ok(mapper.Map<MyInfoResponse>(user));
    }

    public async Task<Result> DeleteUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result.Fail(new[] { "User not found." });

        user.IsDeleted = true;
        user.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync(ct);

        return Result.Ok();
    }

    public async Task<Result> UpdateUserAsync(Guid userId, UpdateMyInfoRequest request, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync(new object[] { userId }, ct);

        if (user == null)
            return Result.Fail(new[] { "User not found." });

        if (request.Username != null)
        {
            var isUsernameTaken = await context.Users
                .AnyAsync(u => u.Username == request.Username && u.Id != userId, ct);

            if (isUsernameTaken)
                return Result.Fail(new[] { "Username is already taken." });

            user.UpdatedAt = DateTime.UtcNow;
            user.Username = request.Username;
        }

        if (request.Email != null)
        {
            var isEmailTaken = await context.Users
                .AnyAsync(u => u.Email == request.Email && u.Id != userId, ct);

            if (isEmailTaken)
                return Result.Fail(new[] { "Email is already taken." });

            user.UpdatedAt = DateTime.UtcNow;
            user.Email = request.Email;
        }

        await context.SaveChangesAsync(ct);

        return Result.Ok();
    }
}