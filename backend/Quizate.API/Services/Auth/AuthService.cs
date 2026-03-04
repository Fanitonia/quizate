using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Quizate.API.Auth;
using Quizate.API.Contracts;
using Quizate.API.Data;
using Quizate.Data.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Quizate.API.Services.Auth;

public class AuthService(
    QuizateDbContext dbContext,
    IConfiguration configuration,
    IPasswordHasher<User> passwordHasher,
    ITokenHasher tokenHasher) : IAuthService
{
    public async Task<bool> RegisterAsync(RegisterRequest request)
    {
        string username = request.Username.Trim();
        string? email = request.Email?.Trim();

        if (!Validation.IsValidUsername(username))
            return false;

        if (email != null && !Validation.IsValidEmail(email))
            return false;

        string normalizedUsername = username.ToLowerInvariant();
        string? normalizedEmail = email?.ToLowerInvariant();

        var isUserExist = await dbContext.Users.AnyAsync(u =>
            u.NormalizedUsername == normalizedUsername
            || (normalizedEmail != null && u.Email != null && u.Email == normalizedEmail));

        if (isUserExist)
            return false;

        var user = new User
        {
            Username = username,
            Email = normalizedEmail,
            PasswordHash = "init"
        };

        string hashedPassword = passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return true;
    }

    public async Task<AuthTokens?> LoginAsync(LoginRequest request)
    {
        string input = request.UsernameOrEmail.Trim();

        if (!Validation.IsValidUsername(input) && !Validation.IsValidEmail(input))
            return null;

        string normalizedInput = input.ToLowerInvariant();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u =>
                u.NormalizedUsername == normalizedInput
                || (u.Email != null && u.Email == normalizedInput));

        if (user == null)
            return null;

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return null;

        var (refreshToken, rawToken) = CreateRefreshToken(user.Id);
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();

        return new AuthTokens
        {
            AccessToken = CreateAccessToken(user),
            RefreshToken = rawToken
        };
    }

    public async Task<AuthTokens?> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return null;

        var refreshTokenHash = tokenHasher.ComputeHash(refreshToken);

        var existing = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == refreshTokenHash);

        if (existing == null || existing.IsExpired)
            return null;

        var (newRefreshToken, rawToken) = CreateRefreshToken(existing.UserId);

        dbContext.RefreshTokens.Add(newRefreshToken);
        dbContext.RefreshTokens.Remove(existing);
        await dbContext.SaveChangesAsync();

        return new AuthTokens
        {
            AccessToken = CreateAccessToken(existing.User),
            RefreshToken = rawToken
        };
    }

    public async Task<bool> RevokeRefreshTokensAsync(Guid userId)
    {
        var userTokens = await dbContext.RefreshTokens.Where(rt => rt.UserId == userId).ToListAsync();

        if (!userTokens.Any())
            return false;

        dbContext.RefreshTokens.RemoveRange(userTokens);
        await dbContext.SaveChangesAsync();

        return true;
    }

    private string CreateAccessToken(User user)
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

    private (RefreshToken, string rawToken) CreateRefreshToken(Guid userId)
    {
        string rawToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));

        return (new RefreshToken
        {
            UserId = userId,
            TokenHash = tokenHasher.ComputeHash(rawToken),
            ExpiresAtUtc = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"))
        }, rawToken);
    }
}
