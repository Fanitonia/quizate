using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Quizate.API.Data;
using Quizate.API.Shared.Result;
using Quizate.Data.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Quizate.API.Services.Auth;

public class TokenManager(
    QuizateDbContext dbContext,
    IConfiguration configuration) : ITokenManager
{
    public async Task<Result<AuthTokens>> RefreshTokenAsync(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return Result<AuthTokens>.Failure(["Could not find a refresh token."]);

        var refreshTokenHash = Hasher.ComputeHash(refreshToken);

        var existing = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == refreshTokenHash);

        if (existing == null || existing.IsExpired)
            return Result<AuthTokens>.Failure(["Invalid refresh token."]);

        var (newRefreshToken, rawToken) = CreateRefreshToken(existing.UserId);

        dbContext.RefreshTokens.Add(newRefreshToken);
        dbContext.RefreshTokens.Remove(existing);
        await dbContext.SaveChangesAsync();

        return Result<AuthTokens>.Success(new AuthTokens
        {
            AccessToken = CreateAccessToken(existing.User),
            RefreshToken = rawToken
        });
    }

    public async Task<Result> RevokeRefreshTokensAsync(Guid userId)
    {
        var userTokens = await dbContext.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();

        if (userTokens.Count == 0)
            return Result.Success();

        dbContext.RefreshTokens.RemoveRange(userTokens);
        await dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public string CreateAccessToken(User user)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:SecretKey")!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes")),
            SigningCredentials = credentials,
            Issuer = configuration.GetValue<string>("Jwt:Issuer"),
            Audience = configuration.GetValue<string>("Jwt:Audience"),
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }

    public (RefreshToken, string rawToken) CreateRefreshToken(Guid userId)
    {
        string rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return (new RefreshToken
        {
            UserId = userId,
            TokenHash = Hasher.ComputeHash(rawToken),
            ExpiresAtUtc = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"))
        }, rawToken);
    }
}

