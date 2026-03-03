namespace Quizate.API.Services
{
    public interface ITokenHasher
    {
        public string ComputeHash(string value);
    }
}
