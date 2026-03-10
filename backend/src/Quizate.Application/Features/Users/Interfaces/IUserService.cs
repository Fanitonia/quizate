using Quizate.Application.Features.Users.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Shared.Result;

namespace Quizate.Application.Features.Users.Interfaces;

public interface IUserService
{
    public Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct);
    public Task<Result<MyInfoResponse>> GetMyInfoAsync(Guid userId, CancellationToken ct);
    public Task<Result> UpdateUserAsync(Guid userId, UpdateMyInfoRequest request, CancellationToken ct);
    public Task<Result> DeleteUserAsync(Guid userId, CancellationToken ct);
}
