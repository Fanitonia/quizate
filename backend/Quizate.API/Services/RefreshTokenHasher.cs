using System.Security.Cryptography;
using System.Text;

namespace Quizate.API.Services
{
    public class RefreshTokenHasher(IConfiguration configuration)
    {
        public string ComputeHash(string token)
        {
            byte[] key = Encoding.UTF8.GetBytes(configuration.GetValue<string>("Jwt:Key")!);
            byte[] data = Encoding.UTF8.GetBytes(token);

            using var hmac = new HMACSHA256(key);
            byte[] hash = hmac.ComputeHash(data);

            return Convert.ToHexString(hash);
        }
    }
}
