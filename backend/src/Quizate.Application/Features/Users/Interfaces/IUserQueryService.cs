using Quizate.Application.Common.Pagination;
using Quizate.Application.Features.Users.DTOs.Responses;

namespace Quizate.Application.Features.Users.Interfaces;

public interface IUserQueryService
{
    public Task<UserInfoResponse?> GetUserAsync(Guid userId, CancellationToken ct);
    public Task<DetailedUserInfoResponse?> GetDetailedUserAsync(Guid userId, CancellationToken ct);
    public Task<PaginatedList<DetailedUserInfoResponse>> GetAllUsersAsync(PaginationParameters pagination, CancellationToken ct);
}
