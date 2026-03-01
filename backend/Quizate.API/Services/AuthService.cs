using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Quizate.API.Contracts;
using Quizate.API.Data;
using Quizate.Data.Enums;
using Quizate.Data.Models;
using System.Security.Claims;
using System.Text;

namespace Quizate.API.Services
{
    public class AuthService(QuizateDbContext _dbContext, IPasswordHasher<User> _hasher, IConfiguration _configuration) : IAuthService
    {
        // TODO: Model validator ekle.
        public async Task<string?> LoginAsync(LoginUserRequest request)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == request.UsernameOrEmail
                || u.Email == request.UsernameOrEmail);

            if (user == null)
                return null;

            var result = _hasher.VerifyHashedPassword(user, user.HashedPassword, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return null;

            return CreateToken(user);
        }

        // TODO: Model validator ekle.
        public async Task<User?> RegisterAsync(RegisterUserRequest request)
        {
            var isUserExist = await _dbContext.Users.AnyAsync(u =>
                u.Username.ToLower() == request.Username.ToLower()
                || (request.Email != null && u.Email != null && u.Email.ToLower() == request.Email.ToLower()));

            if (isUserExist)
                return null;

            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                HashedPassword = "init"
            };

            string hashedPassword = _hasher.HashPassword(user, request.Password);
            user.HashedPassword = hashedPassword;

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = credentials,
                Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
                Audience = _configuration.GetValue<string>("Jwt:Audience"),
            };

            var handler = new JsonWebTokenHandler();

            return handler.CreateToken(tokenDescriptor);
        }
    }
}
