using Home.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string AuthenticateUser(Users _user)
        {
            if(_user.Username=="admin" && _user.Password=="ABC")
            {
                return "Success";
            }
            return "Fail";

        }

        private string GenerateToken(Users _user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], null,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Users _user)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(_user);
            if(user!=null)
            {
                var token = GenerateToken(user);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
}
