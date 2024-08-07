namespace ApiApplication.Services
{
    using ApiApplication.Model;
    using Database;
    using Database.Model;
    using System.Text;

    public class AuthServices : IAuthServices
    {
        private readonly IAesKeyServices aesKeyServices;
        private readonly ApplicationDbContext dbContext;

        public AuthServices(IAesKeyServices aesKeyServices, ApplicationDbContext dbContext)
        {
            this.aesKeyServices = aesKeyServices;
            this.dbContext = dbContext;
        }

        public SqlUser IsUserAuthenticate(string username, string password)
        {
            var result = new SqlUser();
            var user = dbContext.User.Where(x => x.Username == username).FirstOrDefault();
            if (user == null)
            {
                return result;
            }

            var encryptedRequest = this.aesKeyServices.EncryptText(password, user.Id, user.Iv);
            if (this.IsByteSame(encryptedRequest.EncryptedPassword, user.Password))
            {
                result.Id = user.Id;
                result.Username = user.Username;
                result.Password = password;

                return result;
            }

            return result;
        }

        public BaseResponse CreateUser(string username, string password)
        {
            var result = new BaseResponse();
            var user = dbContext.User.Where(x => x.Username == username).FirstOrDefault();
            if (user != null)
            {
                result.setErrorCode("Username already used.");
                return result;
            }

            var encryptedResult = this.aesKeyServices.EncryptText(username);
            var userToBeStored = this.AesToUser(encryptedResult);
            userToBeStored.Username = username;

            dbContext.User.Add(userToBeStored);
            dbContext.SaveChanges();

            result.setSuccessCode();
            return result;
        }

        private User AesToUser(AesEncryptionResultDto dto)
        {
            var result = new User();
            result.Id = dto.Key;
            result.Iv = dto.Iv;
            result.Password = dto.EncryptedPassword;

            return result;
        }

        private bool IsByteSame(byte[] byte1, byte[] byte2)
        {
            if (byte1.Length != byte2.Length)
            {
                return false;
            }

            for (int i = 0; i < byte1.Length; i++)
            {
                if (byte1[i].CompareTo(byte2[i]) != 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
