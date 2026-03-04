using Quizate.API.Contracts;

namespace Quizate.API.Services.Auth;

public interface IUserManager
{
    public Task<AuthResult> RegisterAsync(RegisterRequest request);
    public Task<AuthResult<AuthTokens>> LoginAsync(LoginRequest request);
}
