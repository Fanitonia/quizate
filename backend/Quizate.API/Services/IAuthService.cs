using Quizate.API.Contracts;
using Quizate.Data.Models;

namespace Quizate.API.Services
{
    public interface IAuthService
    {
        public Task<User?> RegisterAsync(RegisterUserRequest request);
        public Task<string?> LoginAsync(LoginUserRequest request);
    }
}
