using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Quizate.Core.Entities.Users;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Quizate.Application.Auth.Helpers;

internal static class TokenProvider
{
    public static string CreateJwtToken(User user, IConfiguration configuration)
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

    public static (RefreshToken, string rawToken) CreateRefreshToken(Guid userId, IConfiguration configuration)
    {
        string rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return (new RefreshToken
        {
            UserId = userId,
            TokenHash = Sha256Hasher.ComputeHash(rawToken),
            ExpiresAtUtc = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"))
        }, rawToken);
    }
}

