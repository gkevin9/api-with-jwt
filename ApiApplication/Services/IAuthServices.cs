namespace ApiApplication.Services
{
    using ApiApplication.Model;

    public interface IAuthServices
    {
        SqlUser IsUserAuthenticate(string username, string password);

        BaseResponse CreateUser(string username, string password);
    }
}
