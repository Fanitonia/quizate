namespace Quizate.API.Contracts
{
    public class RegisterUserRequest
    {
        public required string Username { get; set; }
        public string? Email { get; set; }
        public required string Password { get; set; }
    }
}
