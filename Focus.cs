using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace decryptor
{
    public static class Focus
    {
        private const int Keysize = 256;

        private const int DerivationIterations = 1000;

        public static string Decrypt(string cipherText, string passPhrase)
        {
            try
            {
                byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
                byte[] saltStringBytes = cipherTextBytesWithSaltAndIv.Take(32).ToArray();
                byte[] ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(32).Take(16).ToArray();
                byte[] cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip(32 + 16).Take(cipherTextBytesWithSaltAndIv.Length - (32 + 16)).ToArray();

                Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations);

                byte[] keyBytes = password.GetBytes(Keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 128;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform decryptorU = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptorU, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "Decrypt Error";
            }
        }
    }
}
