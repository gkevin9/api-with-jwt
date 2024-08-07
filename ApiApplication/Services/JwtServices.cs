using ApiApplication.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiApplication.Services
{
    public class JwtServices : IJwtServices
    {
        private readonly IConfiguration configuration;
        private readonly IAesKeyServices aesKeyServices;

        public JwtServices(IConfiguration configuration, IAesKeyServices aesKeyServices)
        {
            this.configuration = configuration;
            this.aesKeyServices = aesKeyServices;
        }

        public string GenerateJwtToken(SqlUser user)
        {
            var userId = this.aesKeyServices.EncryptText(user.Username);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Convert.ToBase64String(userId.Key)),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"])), 
                    SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
