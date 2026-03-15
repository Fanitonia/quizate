using Quizate.Application.Features.Auth.DTOs;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Shared.Result;

namespace Quizate.Application.Features.Auth.Interfaces;

public interface IAuthService
{
    public Task<Result<AuthTokens>> RegisterAsync(RegisterRequest request);
    public Task<Result<AuthTokens>> LoginAsync(LoginRequest request);
    public Task<Result<AuthTokens>> RefreshAccessTokenAsync(string refreshToken);
    public Task<Result> RevokeRefreshTokenAsync(string refreshToken);
    public Task<Result> RevokeAllRefreshTokensAsync(Guid userId);
}
