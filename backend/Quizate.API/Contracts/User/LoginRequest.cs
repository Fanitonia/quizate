namespace Quizate.API.Contracts
{
    public class LoginRequest
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
