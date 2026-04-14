using Quizate.Application.Common.Pagination;
using Quizate.Application.Features.Users.DTOs.Responses;

namespace Quizate.Application.Features.Users.Interfaces;

public interface IUserQueryService
{
    public Task<UserInfoResponse?> GetUserByUsernameAsync(string username, CancellationToken ct);
    public Task<UserInfoResponse?> GetUserByIdAsync(Guid userId, CancellationToken ct);
    public Task<DetailedUserInfoResponse?> GetDetailedUserAsync(Guid userId, CancellationToken ct);
    public Task<PaginatedList<DetailedUserInfoResponse>> GetAllUsersAsync(PaginationParameters pagination, string? username, CancellationToken ct);
}
