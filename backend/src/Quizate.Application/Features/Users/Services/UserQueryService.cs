using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
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

    public async Task<PaginatedList<DetailedUserInfoResponse>> GetAllUsersAsync(PaginationParameters pagination, CancellationToken ct)
    {
        var users = await context.Users
            .AsNoTracking()
            .Skip((pagination.Page - 1) * pagination.Page)
            .Take(pagination.PageSize)
            .ProjectTo<DetailedUserInfoResponse>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

        var totalCount = await context.Users.CountAsync();

        var paginationMetadata = new PaginationMetadata(
            pagination, totalCount);

        return new(paginationMetadata, users);
    }
}
