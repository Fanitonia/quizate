using Quizate.API.Contracts;

namespace Quizate.API.Services.Auth;

public interface IAuthService
{
    public Task<bool> RegisterAsync(RegisterRequest request);
    public Task<AuthTokens?> LoginAsync(LoginRequest request);
    public Task<AuthTokens?> RefreshTokenAsync(string refreshToken);
    public Task<bool> RevokeRefreshTokensAsync(Guid userId);
}
