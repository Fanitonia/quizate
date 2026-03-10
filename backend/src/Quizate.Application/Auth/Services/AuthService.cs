using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Quizate.Application.Auth.DTOs;
using Quizate.Application.Auth.DTOs.Requests;
using Quizate.Application.Auth.Helpers;
using Quizate.Application.Auth.Interfaces;
using Quizate.Application.Shared.Result;
using Quizate.Core.Entities.Users;
using Quizate.Persistence;

namespace Quizate.Application.Auth.Services;

public class AuthService(
    QuizateDbContext dbContext,
    IPasswordHasher<User> passwordHasher,
    IConfiguration configuration) : IAuthService
{
    public async Task<Result> RegisterAsync(RegisterRequest request)
    {
        string normalizedUsername = request.Username.ToLowerInvariant();
        string? normalizedEmail = request.Email?.ToLowerInvariant();

        var isUserExist = await dbContext.Users.AnyAsync(u =>
            u.NormalizedUsername == normalizedUsername
            || (normalizedEmail != null && u.Email != null && u.Email == normalizedEmail));

        if (isUserExist)
            return Result.Failure("User already exist.");

        var user = new User
        {
            Username = request.Username,
            Email = normalizedEmail,
            PasswordHash = "init"
        };

        string hashedPassword = passwordHasher.HashPassword(user, request.Password);
        user.PasswordHash = hashedPassword;

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

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return Result<AuthTokens>.Failure("Invalid username/email or password.");

        var (refreshToken, rawToken) = TokenProvider.CreateRefreshToken(user.Id, configuration);
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();

        return Result<AuthTokens>.Success(new AuthTokens
        {
            AccessToken = TokenProvider.CreateJwtToken(user, configuration),
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

        var (newRefreshToken, rawToken) = TokenProvider.CreateRefreshToken(existing.UserId, configuration);

        dbContext.RefreshTokens.Add(newRefreshToken);
        dbContext.RefreshTokens.Remove(existing);
        await dbContext.SaveChangesAsync();

        return Result<AuthTokens>.Success(new AuthTokens
        {
            AccessToken = TokenProvider.CreateJwtToken(existing.User, configuration),
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
