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
}