using Quizate.API.Contracts;

namespace Quizate.API.Services.Auth;

public interface IAuthService
{
    public Task<AuthResult> RegisterAsync(RegisterRequest request);
    public Task<AuthResult<AuthTokens>> LoginAsync(LoginRequest request);
    public Task<AuthResult<AuthTokens>> RefreshTokenAsync(string? refreshToken);
    public Task<AuthResult> RevokeRefreshTokensAsync(Guid userId);
}
