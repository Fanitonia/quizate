using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Requests;
using Quizate.Application.Features.Users.DTOs.Responses;
using Quizate.Application.Shared.Result;

namespace Quizate.Application.Features.Users.Interfaces;

public interface IUserService
{
    public Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct);
    public Task<Result<DetailedUserInfoResponse>> GetDetailedUserAsync(Guid userId, CancellationToken ct);
    public Task<Result> UpdateUserAsync(Guid userId, UpdateUserRequest request);
    public Task<Result> DeleteUserAsync(Guid userId);
    public Task<Result> ChangePasswordAsync(PasswordChangeRequest request, Guid userId);
}
