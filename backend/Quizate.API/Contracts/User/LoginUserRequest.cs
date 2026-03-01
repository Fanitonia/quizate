namespace Quizate.API.Contracts
{
    public class LoginUserRequest
    {
        public required string UsernameOrEmail { get; set; }
        public required string Password { get; set; }
    }
}
