using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityAPI.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Route("[controller]/[action]")]
    public class PermissionController : Controller
    {
        [Authorize(Roles = SecurityRoles.Admin)]
        [HttpGet("AdminRole")]
        public IActionResult AdminRole()
        {
            return Ok("Hello Admin");
        }

        [Authorize(Roles = SecurityRoles.Manager)]
        [HttpGet("ManagerRole")]
        public IActionResult ManagerRole()
        {
            return Ok("Hello Manager");
        }

       [Authorize(Roles = SecurityRoles.User)]
        [HttpGet("UserRole")]
        public IActionResult UserRole()
        {
            return Ok("Hello User");
        }
    }
}
