namespace ApiApplication.Services
{
    using ApiApplication.Model;

    public interface IAesKeyServices
    {
        public AesEncryptionResultDto EncryptText(string text);

        public AesEncryptionResultDto EncryptText(string text, byte[] key, byte[] iv);

        // somehow not work
        public string DecryptText(byte[] encrypted, byte[] key, byte[] iv);
    }
}
