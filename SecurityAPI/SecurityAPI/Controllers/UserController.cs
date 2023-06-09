using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SecurityAPI.Constants;
using SecurityAPI.Entities;
using SecurityAPI.Helpers;
using SecurityAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<IActionResult> InitRole()
        {
            foreach (var role in SecurityRoles.Roles)
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    await _roleManager.CreateAsync(new ApplicationRole
                    {
                        Name = role,
                        NormalizedName = role.ToUpper(),
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        Description = $"Role for {role}"
                    });
                }
            }

            return Ok("Done");
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserRegister dto)
        {
            var existUsername = await _userManager.FindByNameAsync(dto.UserName);

            if (existUsername != null) return new BadRequestObjectResult($"Username {dto.UserName} has already been taken");

            var appUser = UserHelper.ToApplicationUser(dto);

            var result1 = await _userManager.CreateAsync(appUser, dto.Password);

            if (!result1.Succeeded) return new BadRequestObjectResult(result1.Errors);

            List<string> roles = new();

            if (dto.IsAdmin) roles.AddRange(SecurityRoles.Roles.ToList());
            else roles.Add("User");

            var result2 = await _userManager.AddToRolesAsync(appUser, roles);

            if (!result2.Succeeded) return new BadRequestObjectResult(result2.Errors);

            var response = await _userManager.FindByNameAsync(dto.UserName);

            return Ok(response);
        }
    }
}
