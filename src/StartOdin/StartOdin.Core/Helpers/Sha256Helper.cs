using System.Security.Cryptography;
using System.Text;

namespace StartOdin.Core.Helpers;

public class Sha256Helper
{
    public static string ToHash(string? value)
    {
        var sb = new StringBuilder();

        using (var hash = SHA256.Create())            
        {
            Encoding enc = Encoding.UTF8;
            byte[] result = hash.ComputeHash(enc.GetBytes(value));

            foreach (byte b in result)
                sb.Append(b.ToString("x2"));
        }

        return sb.ToString();
    }
}