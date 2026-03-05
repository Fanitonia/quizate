using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Quizate.API.Contracts;
using Quizate.API.Data;
using Quizate.Data.Models;

namespace Quizate.API.Services.Auth;

public class UserManager(
    QuizateDbContext dbContext,
    IPasswordHasher<User> passwordHasher,
    ITokenManager tokenManager) : IUserManager
{
    public async Task<AuthResult> RegisterAsync(RegisterRequest request)
    {

        string normalizedUsername = request.Username.ToLowerInvariant();
        string? normalizedEmail = request.Email?.ToLowerInvariant();

        var isUserExist = await dbContext.Users.AnyAsync(u =>
            u.NormalizedUsername == normalizedUsername
            || (normalizedEmail != null && u.Email != null && u.Email == normalizedEmail));

        if (isUserExist)
            return AuthResult.Fail(["User already exist."]);

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

        return AuthResult.Ok();
    }

    public async Task<AuthResult<AuthTokens>> LoginAsync(LoginRequest request)
    {
        string normalizedInput = request.UsernameOrEmail.Trim().ToLowerInvariant();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u =>
                u.NormalizedUsername == normalizedInput
                || (u.Email != null && u.Email == normalizedInput));

        if (user == null)
            return AuthResult<AuthTokens>.Fail(["Invalid username/email or password."]);

        var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

        if (result == PasswordVerificationResult.Failed)
            return AuthResult<AuthTokens>.Fail(["Invalid username/email or password."]);

        var (refreshToken, rawToken) = tokenManager.CreateRefreshToken(user.Id);
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();

        return AuthResult<AuthTokens>.Ok(new AuthTokens
        {
            AccessToken = tokenManager.CreateAccessToken(user),
            RefreshToken = rawToken
        });
    }
}
