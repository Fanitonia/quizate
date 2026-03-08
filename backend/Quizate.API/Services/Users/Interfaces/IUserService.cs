using Quizate.API.Contracts;
using Quizate.API.Shared.Result;

namespace Quizate.API.Services.Users;

public interface IUserService
{
    public Task<Result<UserInfoResponse>> GetUserAsync(Guid userId, CancellationToken ct);
}
