namespace Quizate.API.Services.Auth;

public interface ITokenHasher
{
    public string ComputeHash(string value);
}
