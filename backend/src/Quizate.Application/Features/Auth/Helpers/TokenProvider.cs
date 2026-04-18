using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Quizate.Application.Features.Auth.DTOs;
using Quizate.Domain.Entities.Users;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Quizate.Application.Features.Auth.Helpers;

internal static class TokenProvider
{
    public static string CreateJwtToken(User user, JwtSettings jwtSettings)
    {
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(jwtSettings.ExpirationMinutes),
            SigningCredentials = credentials,
            Issuer = jwtSettings.Issuer,
            Audience = jwtSettings.Audience,
        };

        var handler = new JsonWebTokenHandler();

        return handler.CreateToken(tokenDescriptor);
    }

    public static (RefreshToken, string rawToken) CreateRefreshToken(Guid userId, int expirationDays)
    {
        string rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return
            (new RefreshToken(
                Sha256Hasher.ComputeHash(rawToken),
                userId,
                DateTime.UtcNow.AddDays(expirationDays)),
            rawToken);
    }
}
