using Quizate.API.Shared.Result;
using Quizate.Data.Models;

namespace Quizate.API.Services.Auth;

public interface ITokenManager
{
    public Task<Result<AuthTokens>> RefreshTokenAsync(string? refreshToken);

    public Task<Result> RevokeRefreshTokensAsync(Guid userId);

    public string CreateAccessToken(User user);

    public (RefreshToken, string rawToken) CreateRefreshToken(Guid userId);
}
