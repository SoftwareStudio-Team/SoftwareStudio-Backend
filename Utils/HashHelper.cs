using System.Security.Cryptography;
using System.Text;

namespace Backend.Utils
{
    public class HashHelper
    {
        public static string Hash(string text, string algorithm = "sha256")
        {
            using (var hashAlgorithm = HashAlgorithm.Create(algorithm))
            {
                return Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(text)));
            }
        }
    }
}