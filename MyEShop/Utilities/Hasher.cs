using System.Security.Cryptography;
using System.Text;

namespace MyEShop.Utilities
{
    public static class Hasher
    {
        public static string HashString(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            using (var algorithm = SHA512.Create())
            {
                byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
                return ConvertBytesToHexString(hashedBytes);
            }
        }

        public static bool CompareHashes(string hash1, string hash2)
        {
            if (hash1.Length != hash2.Length)
            {
                return false;
            }

            for (int i = 0; i < hash1.Length; i += 2)
            {
                string byte1 = hash1.Substring(i, 2);
                string byte2 = hash2.Substring(i, 2);
                if (!string.Equals(byte1, byte2, StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
            }

            return true;
        }

        private static string ConvertBytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}
