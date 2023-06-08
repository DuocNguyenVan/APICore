using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecurityAPI.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("[controller]/[action]")]
    public class PermissionController : Controller
    {
        [Authorize(Roles = SecurityRoles.Admin)]
        [HttpGet]
        public IActionResult AdminRole()
        {
            return Ok("Hello Admin");
        }

        [Authorize(Roles = SecurityRoles.Manager)]
        [HttpGet]
        public IActionResult ManagerRole()
        {
            return Ok("Hello Manager");
        }

       // [Authorize(Roles = SecurityRoles.User)]
        [HttpGet]
        public IActionResult UserRole()
        {
            return Ok("Hello User");
        }
    }
}
