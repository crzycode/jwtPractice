using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Jwt.Model;

namespace Jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Authorize(Roles ="admin")]
        [HttpGet("Admins")]
        public IActionResult adminEndpoint()
        {
            var currentuser = getcurrentuser();
            return Ok($"{currentuser.GivenName}, you are an {currentuser.Role}");
        }
        [Authorize(Roles = "seller")]
        [HttpGet("seller")]
        public IActionResult sellerEndpoint()
        {
            var currentuser = getcurrentuser();
            return Ok($"{currentuser.GivenName}, you are an {currentuser.Role}");
        }

        [Authorize(Roles = "seller,admin")]
        [HttpGet("adminseller")]
        public IActionResult adminseller()
        {
            var currentuser = getcurrentuser();
            return Ok($"{currentuser.GivenName}, you are an {currentuser.Role}");
        }

        [HttpGet("public")]
       public IActionResult Publics()
        {
            return Ok("hello");
        }
        private UserModel getcurrentuser()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if(identity != null)
            {
                var userClaims = identity.Claims;
                return new UserModel
                {
                    Username = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    EmailAddress = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    GivenName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.GivenName)?.Value,
                    Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                    Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,

                };
            }return null;

        }
    }
}
