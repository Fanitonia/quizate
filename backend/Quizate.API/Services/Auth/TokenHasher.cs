using System.Security.Cryptography;
using System.Text;

namespace Quizate.API.Services.Auth;

public class TokenHasher : ITokenHasher
{
    public string ComputeHash(string value)
    {
        ArgumentNullException.ThrowIfNull(value);

        byte[] inputBytes = Encoding.UTF8.GetBytes(value);
        byte[] hashBytes = SHA256.HashData(inputBytes);

        return Convert.ToHexString(hashBytes);
    }
}
