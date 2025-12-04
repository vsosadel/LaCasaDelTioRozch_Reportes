using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Utils.Security
{
    public static class Encrypt
    {
        private static byte[] InitVectorBytes = Array.Empty<byte>();
        private static byte[] SaltValueBytes = Array.Empty<byte>();
        private const int KeySize = 256;
        private const int DerivationIterations = 1000;
        private static string PassPhrase = string.Empty;

        public static void Configure(IConfiguration configuration)
        {
            PassPhrase = configuration["EncryptionSettings:PassPhrase"] ?? throw new InvalidOperationException("PassPhrase cannot be null");
            InitVectorBytes = Encoding.ASCII.GetBytes(configuration["EncryptionSettings:InitVectorBytes"] ?? throw new InvalidOperationException("InitVectorBytes cannot be null"));
            SaltValueBytes = Encoding.ASCII.GetBytes(configuration["EncryptionSettings:SaltValueBytes"] ?? throw new InvalidOperationException("SaltValueBytes cannot be null"));
        }

        public static string Encriptar(string? cadena)
        {
            if (string.IsNullOrEmpty(cadena)) { return string.Empty; }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(cadena);
            using (var password = new Rfc2898DeriveBytes(PassPhrase, SaltValueBytes, DerivationIterations, HashAlgorithmName.SHA512))
            {
                byte[] keyBytes = password.GetBytes(KeySize / 8);
                using (var aesAlgorithm = Aes.Create())
                {
                    aesAlgorithm.Mode = CipherMode.CBC;
                    using (var encryptor = aesAlgorithm.CreateEncryptor(keyBytes, InitVectorBytes))
                    using (var memoryStream = new MemoryStream())
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        return Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }
        }

        public static string Desencriptar(string? cadena)
        {
            if (string.IsNullOrEmpty(cadena)) { return string.Empty; }

            byte[] cipherTextBytes = Convert.FromBase64String(cadena);
            using (var password = new Rfc2898DeriveBytes(PassPhrase, SaltValueBytes, DerivationIterations, HashAlgorithmName.SHA512))
            {
                byte[] keyBytes = password.GetBytes(KeySize / 8);
                using (var aesAlgorithm = Aes.Create())
                {
                    aesAlgorithm.Mode = CipherMode.CBC;
                    using (var decryptor = aesAlgorithm.CreateDecryptor(keyBytes, InitVectorBytes))
                    using (var memoryStream = new MemoryStream(cipherTextBytes))
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                    }
                }
            }
        }
    }
}