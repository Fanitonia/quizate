using Quizate.Application.Features.Auth.DTOs;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Shared.Result;

namespace Quizate.Application.Features.Auth.Interfaces;

public interface IAuthService
{
    public Task<Result> RegisterAsync(RegisterRequest request);
    public Task<Result<AuthTokens>> LoginAsync(LoginRequest request);
    public Task<Result<AuthTokens>> RefreshAccessTokenAsync(string? refreshToken);
    public Task<Result> RevokeRefreshTokensAsync(Guid userId);
}
