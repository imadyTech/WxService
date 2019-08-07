using System;
using System.Text;

namespace imady.Common
{
    public class SHA256
    {
        public static string Compute(string data)
        {
            var sha256 = System.Security.Cryptography.SHA256.Create();
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
