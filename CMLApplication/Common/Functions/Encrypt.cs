using System.Security.Cryptography;
using System.Text;

namespace CMLApplication.Common.Functions
{
    public class Encrypt
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // Converte a senha para um array de bytes
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Converte o array de bytes em uma string hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
