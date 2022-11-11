using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Logins([FromBody] UserLogin userlogin)
        {
            var user = Authentication(userlogin);
            if(user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }
            return NotFound("user Not FOund");
        }

        private string Generate(UserModel user)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Email,user.EmailAddress),
                new Claim(ClaimTypes.GivenName,user.GivenName),
                new Claim(ClaimTypes.Surname,user.Surname),
                new Claim(ClaimTypes.Role,user.Role)

            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
           
        }

        private UserModel Authentication(UserLogin userlogin)
        {
            var currentuser = UserConstant.Users.FirstOrDefault(o => o.Username.ToLower()
            == userlogin.UserName.ToLower() && o.Password == userlogin.Password);
            if(currentuser != null)
            {
                return currentuser;
            }
            return null;
        }
    }
}
