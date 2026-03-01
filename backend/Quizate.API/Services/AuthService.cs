using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Quizate.API.Contracts;
using Quizate.API.Contracts.User;
using Quizate.API.Data;
using Quizate.Data.Enums;
using Quizate.Data.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Quizate.API.Services
{
    public class AuthService(
        QuizateDbContext dbContext,
        IConfiguration configuration,
        IPasswordHasher<User> passwordHasher,
        RefreshTokenHasher refreshTokenHasher) : IAuthService
    {
        // TODO: Model validator ekle.
        public async Task<User?> RegisterAsync(RegisterRequest request)
        {
            var isUserExist = await dbContext.Users.AnyAsync(u =>
                u.Username.ToLower() == request.Username.ToLower()
                || (request.Email != null && u.Email != null && u.Email.ToLower() == request.Email.ToLower()));

            if (isUserExist)
                return null;

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                PasswordHash = "init"
            };

            string hashedPassword = passwordHasher.HashPassword(user, request.Password);
            user.PasswordHash = hashedPassword;

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        // TODO: Model validator ekle.
        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == request.UsernameOrEmail
                || u.Email == request.UsernameOrEmail);

            if (user == null)
                return null;

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            var (refreshToken, rawToken) = CreateRefreshToken(user.Id);
            dbContext.RefreshTokens.Add(refreshToken);
            await dbContext.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = CreateAccessToken(user),
                RefreshToken = rawToken
            };
        }

        public async Task<LoginResponse?> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var refreshTokenHash = refreshTokenHasher.ComputeHash(request.RefreshToken);

            var existing = await dbContext.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.TokenHash == refreshTokenHash);

            if (existing == null || existing.IsExpired)
                return null;

            var (newRefreshToken, rawToken) = CreateRefreshToken(existing.UserId);

            dbContext.RefreshTokens.Add(newRefreshToken);
            dbContext.RefreshTokens.Remove(existing);
            await dbContext.SaveChangesAsync();

            return new LoginResponse
            {
                AccessToken = CreateAccessToken(existing.User),
                RefreshToken = rawToken
            };
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
                Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")!));
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
                TokenHash = refreshTokenHasher.ComputeHash(rawToken),
                ExpiresAtUtc = DateTime.UtcNow.AddDays(configuration.GetValue<int>("Jwt:RefreshTokenExpirationDays"))
            }, rawToken);
        }
    }
}
