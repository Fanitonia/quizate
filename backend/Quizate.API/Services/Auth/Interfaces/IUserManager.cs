using Quizate.API.Contracts;
using Quizate.API.Shared.Result;

namespace Quizate.API.Services.Auth;

public interface IUserManager
{
    public Task<Result> RegisterAsync(RegisterRequest request);
    public Task<Result<AuthTokens>> LoginAsync(LoginRequest request);
}
