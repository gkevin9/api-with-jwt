namespace ApiApplication.Services
{
    using ApiApplication.Model;

    public interface IJwtServices
    {
        string GenerateJwtToken(SqlUser user);
    }
}
