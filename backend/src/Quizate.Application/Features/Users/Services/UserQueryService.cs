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
    public async Task<UserInfoResponse?> GetUserByUsernameAsync(string username, CancellationToken ct)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == username || u.NormalizedUsername == username.ToLowerInvariant(), ct);

        if (user == null)
            return null;

        return mapper.Map<UserInfoResponse>(user);
    }
    public async Task<UserInfoResponse?> GetUserByIdAsync(Guid userId, CancellationToken ct)
    {
        var user = await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId, ct);

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

    public async Task<PaginatedList<DetailedUserInfoResponse>> GetAllUsersAsync(PaginationParameters pagination, string? username, CancellationToken ct)
    {
        var baseQuery = context.Users
            .AsNoTracking();

        if (username != null)
        {
            var normalizedUsername = username.ToLowerInvariant();
            baseQuery = baseQuery.Where(u => u.NormalizedUsername == normalizedUsername || u.Username == username);
        }

        var users = await baseQuery
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<DetailedUserInfoResponse>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

        var totalCount = await baseQuery.CountAsync(ct);

        var paginationMetadata = new PaginationMetadata(
            pagination, totalCount);

        return new(paginationMetadata, users);
    }
}
