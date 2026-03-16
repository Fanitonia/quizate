using AutoMapper;
using Quizate.Application.Common.Pagination;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Features.Users.Interfaces;
using Quizate.Persistence;

namespace Quizate.Application.Features.Users.Services;

public class UserQueryService(
    QuizateDbContext context,
    IMapper mapper) : IUserQueryService
{
    public async Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync([userId], ct);

        if (user == null)
            return Result<UserInfoResponse>.Failure("User not found.");

        return Result<UserInfoResponse>.Success(mapper.Map<UserInfoResponse>(user));
    }

    public async Task<Result<DetailedUserInfoResponse>> GetDetailedUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync([userId], ct);

        if (user == null)
            return Result<DetailedUserInfoResponse>.Failure("User not found.");

        return Result<DetailedUserInfoResponse>.Success(mapper.Map<DetailedUserInfoResponse>(user));
    }

    public Task<Result<PaginatedList<DetailedUserInfoResponse>>> GetAllUsersAsync(PaginationParameters pagination, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
