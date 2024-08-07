namespace ApiApplication.Model
{
    public class AesEncryptionResultDto
    {
        public byte[] EncryptedPassword { get; set; }

        public byte[] Key { get; set; }

        public byte[] Iv { get; set; }
    }
}
