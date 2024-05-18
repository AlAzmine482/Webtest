using Azure.Identity;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Webtest.DTO;
using Webtest.Models;

namespace Webtest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(UserManager<WorldCarsUser> userManager,
                                     JwtHandeler jwtHandeler) : ControllerBase
    {
        [HttpPost("Login")]
        
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {

            WorldCarsUser? user = await userManager.FindByNameAsync(loginRequest.userName);
            if (user == null)
            {
                return Unauthorized("Wrong Username");

            }
            bool success = await userManager.CheckPasswordAsync(user, loginRequest.password);
            if (!success)
            {
                return Unauthorized("Wrong Password");
            }
            
            JwtSecurityToken token = await jwtHandeler.GetTokenAsync(user);
            var jwtstring = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(new LoginResult { 
                Success = true,
                Message = "heh",
                Token = jwtstring
            });
        }
    }
}