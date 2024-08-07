namespace ApiApplication.Controllers
{
    using ApiApplication.Model;
    using ApiApplication.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private IJwtServices jwtServices;
        private IAuthServices authServices;

        public AuthController(IJwtServices jwtServices, IAuthServices authServices)
        {
            this.jwtServices = jwtServices;
            this.authServices = authServices;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login(LoginUser user)
        {
            var result = new BaseResponse();
            if (user.IsUserDataIncomplete())
            {
                result.setErrorCode("Data incomplete.");
                return BadRequest(result);
            }

            var authResult = this.authServices.IsUserAuthenticate(user.Username, user.Password);
            if (string.IsNullOrEmpty(authResult.Username))
            {
                result.setErrorCode("Username or password is incorrect.");
                return Ok(result);
            }

            var jwtToken = this.jwtServices.GenerateJwtToken(authResult);
            HttpContext.Response.Cookies.Append("loginCookie", jwtToken);
            result.setSuccessCode();

            return Ok(result);
        }

        [HttpPost(Name = "Sign Up")]
        public IActionResult SignUp(LoginUser user)
        {
            var result = new BaseResponse();
            if (user.IsUserDataIncomplete())
            {
                result.setErrorCode("Data incomplete.");
                return BadRequest(result);
            }

            result = this.authServices.CreateUser(user.Username, user.Password);

            return Ok(result);
        }
    }
}
