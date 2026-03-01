using Quizate.API.Contracts;
using Quizate.API.Contracts.User;
using Quizate.Data.Models;

namespace Quizate.API.Services
{
    public interface IAuthService
    {
        public Task<User?> RegisterAsync(RegisterRequest request);
        public Task<LoginResponse?> LoginAsync(LoginRequest request);
        public Task<LoginResponse?> RefreshTokenAsync(RefreshTokenRequest request);
    }
}
