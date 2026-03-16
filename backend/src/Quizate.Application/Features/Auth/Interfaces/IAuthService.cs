using Quizate.Application.Common.Result;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Auth.DTOs.Responses;

namespace Quizate.Application.Features.Auth.Interfaces;

public interface IAuthService
{
    public Task<Result<AuthTokensResponse>> RegisterAsync(RegisterRequest request);
    public Task<Result<AuthTokensResponse>> LoginAsync(LoginRequest request);
    public Task<Result<AuthTokensResponse>> RefreshAccessTokenAsync(string refreshToken);
    public Task<Result> RevokeRefreshTokenAsync(string refreshToken);
    public Task<Result> RevokeAllRefreshTokensAsync(Guid userId);
}
