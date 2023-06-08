using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SecurityAPI.Entities;
using SecurityAPI.Helpers;
using SecurityAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    [Route("[controller]/[action]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthenticationController(UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userLogin)
        {
            var user = await _userManager.FindByNameAsync(userLogin.UserName);

            if (user == null) return new BadRequestObjectResult("Username or Password incorrect");

            var checkPassword = await _userManager.CheckPasswordAsync(user, userLogin.Password);

            if (!checkPassword) return new BadRequestObjectResult("Username or Password incorrect");

            IList<string>? userRoles = await _userManager.GetRolesAsync(user);

            var token = TokenHelper.GenerateToken(
                _configuration["JWT:Secret"]
                , _configuration["JWT:ValidIssuer"]
                , _configuration["JWT:ValidAudience"]
                , userRoles
                , user.Id.ToString()
                , user.UserName
                , user.FullName);

            return Ok(new
            {
                Token = token,
                ValidTo = TokenHelper.GetValidTo(token)
            });
        }
    }
}
