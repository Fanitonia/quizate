using Quizate.Application.Common.Pagination;
using Quizate.Application.Common.Result;
using Quizate.Application.Features.Users.DTOs.Responses;

namespace Quizate.Application.Features.Users.Interfaces;

public interface IUserQueryService
{
    public Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct);
    public Task<Result<DetailedUserInfoResponse>> GetDetailedUserAsync(Guid userId, CancellationToken ct);
    public Task<Result<PaginatedList<DetailedUserInfoResponse>>> GetAllUsersAsync(PaginationParameters pagination, CancellationToken ct);
}
