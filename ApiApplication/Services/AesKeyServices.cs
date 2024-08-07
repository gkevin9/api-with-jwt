namespace ApiApplication.Services
{
    using ApiApplication.Model;
    using System.Security.Cryptography;
    using System.Text;

    public class AesKeyServices : IAesKeyServices
    {
        public AesEncryptionResultDto EncryptText(string text)
        {
            var key = this.GenerateRandomNumber(32);
            var iv = this.GenerateRandomNumber(16);
            return this.Encrypt(text, key, iv);
        }

        public AesEncryptionResultDto EncryptText(string plainText, byte[] key, byte[] iv)
        {
            return this.Encrypt(plainText, key, iv);
        }

        public string DecryptText(byte[] encrypted, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor();

                byte[] result;
                using (var msDecrypt = new MemoryStream(encrypted))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, encryptor, CryptoStreamMode.Read))
                    {
                        using (var msPlain = new MemoryStream())
                        {
                            csDecrypt.CopyTo(msPlain);
                            result = msPlain.ToArray();
                        }
                    }
                }

                return Encoding.UTF8.GetString(result);
            }
        }

        private AesEncryptionResultDto Encrypt(string plainText, byte[] key, byte[] iv)
        {
            var result = new AesEncryptionResultDto();
            result.Key = key;
            result.Iv = iv;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor();

                using (var msEncrypt = new System.IO.MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainByte = Encoding.UTF8.GetBytes(plainText);
                        csEncrypt.Write(plainByte, 0, plainByte.Length);
                    }

                    result.EncryptedPassword = msEncrypt.ToArray();
                }

                return result;
            }
        }

        private byte[] GenerateRandomNumber(int length)
        {
            byte[] result = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(result);
            }

            return result;
        }
    }
}
