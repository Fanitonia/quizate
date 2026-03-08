using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Data;
using Quizate.API.Shared.Result;
using Quizate.Data.Models;

namespace Quizate.API.Services.Auth;

public class UserManager(
    QuizateDbContext dbContext,
    IPasswordHasher<User> passwordHasher,
    ITokenManager tokenManager) : IUserManager
{
    public async Task<Result> RegisterAsync(RegisterRequest request)
    {

        string normalizedUsername = request.Username.ToLowerInvariant();
        string? normalizedEmail = request.Email?.ToLowerInvariant();

        var isUserExist = await dbContext.Users.AnyAsync(u =>
            u.NormalizedUsername == normalizedUsername
            || (normalizedEmail != null && u.Email != null && u.Email == normalizedEmail));

        if (isUserExist)
            return Result.Fail(["User already exist."]);

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

        return Result.Ok();
    }

    public async Task<Result<AuthTokens>> LoginAsync(LoginRequest request)
    {
        string normalizedInput = request.UsernameOrEmail.Trim().ToLowerInvariant();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u =>
                u.NormalizedUsername == normalizedInput
                || (u.Email != null && u.Email == normalizedInput));

        if (user == null)
            return Result<AuthTokens>.Fail(["Invalid username/email or password."]);

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return Result<AuthTokens>.Fail(["Invalid username/email or password."]);

        var (refreshToken, rawToken) = tokenManager.CreateRefreshToken(user.Id);
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();

        return Result<AuthTokens>.Ok(new AuthTokens
        {
            AccessToken = tokenManager.CreateAccessToken(user),
            RefreshToken = rawToken
        });
    }
}
