using AutoMapper;
using Quizate.Application.Common.Pagination;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Features.Users.Interfaces;
using Quizate.Persistence;

namespace Quizate.Application.Features.Users.Services;

public class UserQueryService(
    QuizateDbContext context,
    IMapper mapper) : IUserQueryService
{
    public async Task<UserInfoResponse?> GetUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync([userId], ct);

        if (user == null)
            return null;

        return mapper.Map<UserInfoResponse>(user);
    }

    public async Task<DetailedUserInfoResponse?> GetDetailedUserAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .FindAsync([userId], ct);

        if (user == null)
            return null;

        return mapper.Map<DetailedUserInfoResponse>(user);
    }

    public Task<PaginatedList<DetailedUserInfoResponse>> GetAllUsersAsync(PaginationParameters pagination, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
