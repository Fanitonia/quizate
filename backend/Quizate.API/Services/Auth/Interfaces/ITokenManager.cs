using Humanizer.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Quizate.Data.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Quizate.API.Services.Auth;

public interface ITokenManager
{
    public Task<AuthResult<AuthTokens>> RefreshTokenAsync(string? refreshToken);

    public Task<AuthResult> RevokeRefreshTokensAsync(Guid userId);

    public string CreateAccessToken(User user);

    public (RefreshToken, string rawToken) CreateRefreshToken(Guid userId);
}
