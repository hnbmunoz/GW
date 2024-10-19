
using System.Security.Cryptography;
using System.Text;

namespace MLAB.PlayerEngagement.Infrastructure.Utilities
{
    public class StringConverter
    {
        public static string StringToSHA256(string inputString)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
