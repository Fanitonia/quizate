using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quizate.Application.Features.Auth.DTOs;
using Quizate.Application.Features.Auth.DTOs.Requests;
using Quizate.Application.Features.Auth.Helpers;
using Quizate.Application.Features.Auth.Interfaces;
using Quizate.Application.Shared.Result;
using Quizate.Domain.Entities.Users;
using Quizate.Persistence;

namespace Quizate.Application.Features.Auth.Services;

public class AuthService(
    QuizateDbContext dbContext,
    IPasswordHasher<User> passwordHasher,
    IConfiguration configuration) : IAuthService
{
    private readonly JwtSettings jwtSettings = new()
    {
        SecretKey = configuration.GetValue<string>("Jwt:SecretKey")!,
        ExpirationMinutes = configuration.GetValue<int>("Jwt:AccessTokenExpirationMinutes"),
        Issuer = configuration.GetValue<string>("Jwt:Issuer")!,
        Audience = configuration.GetValue<string>("Jwt:Audience")!
    };

    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        string normalizedUsername = request.Username.ToLowerInvariant();
        string? normalizedEmail = request.Email?.ToLowerInvariant();

        var isUserExist = await dbContext.Users.AnyAsync(u =>
            u.NormalizedUsername == normalizedUsername
            || (normalizedEmail != null && u.Email != null && u.Email == normalizedEmail));

        if (isUserExist)
            return Result.Failure("User already exist.");

        var user = new User(normalizedUsername, "password_placeholder", normalizedEmail);

        string hashedPassword = passwordHasher.HashPassword(user, request.Password);
        user.UpdatePasswordHash(hashedPassword);

        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        return Result.Success();
    }

    public async Task<Result<AuthTokens>> LoginAsync(LoginRequest request)
    {
        string normalizedInput = request.UsernameOrEmail.Trim().ToLowerInvariant();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u =>
                u.NormalizedUsername == normalizedInput
                || (u.Email != null && u.Email == normalizedInput));

        if (user == null)
            return Result<AuthTokens>.Failure("Invalid username/email or password.");

        var passwordResult = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
            return Result<AuthTokens>.Failure("Invalid username/email or password.");

        var accessToken = TokenProvider.CreateJwtToken(user, jwtSettings);
        var (refreshToken, rawToken) = TokenProvider.CreateRefreshToken(user.Id, configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"));

        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();

        return Result<AuthTokens>.Success(new AuthTokens
        {
            AccessToken = accessToken,
            RefreshToken = rawToken
        });
    }

    public async Task<Result<AuthTokens>> RefreshAccessTokenAsync(string? refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            return Result<AuthTokens>.Failure(["Could not find a refresh token."]);

        var refreshTokenHash = Sha256Hasher.ComputeHash(refreshToken);

        var existing = await dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.TokenHash == refreshTokenHash);

        if (existing == null || existing.IsExpired)
            return Result<AuthTokens>.Failure(["Invalid refresh token."]);

        var accessToken = TokenProvider.CreateJwtToken(existing.User, jwtSettings);
        var (newRefreshToken, rawToken) = TokenProvider.CreateRefreshToken(existing.UserId,
            configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"));

        dbContext.RefreshTokens.Add(newRefreshToken);
        dbContext.RefreshTokens.Remove(existing);
        await dbContext.SaveChangesAsync();

        return Result<AuthTokens>.Success(new AuthTokens
        {
            AccessToken = accessToken,
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
}
