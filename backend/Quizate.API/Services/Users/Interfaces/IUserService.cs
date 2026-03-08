using Quizate.API.Contracts;
using Quizate.API.Shared.Result;

namespace Quizate.API.Services.Users;

public interface IUserService
{
    public Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct);
    public Task<Result<MyInfoResponse>> GetMyInfoAsync(Guid userId, CancellationToken ct);
    public Task<Result> UpdateUserAsync(Guid userId, UpdateMyInfoRequest request, CancellationToken ct);
    public Task<Result> DeleteUserAsync(Guid userId, CancellationToken ct);
}
