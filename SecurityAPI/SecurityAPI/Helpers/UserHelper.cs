using SecurityAPI.Entities;
using SecurityAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Helpers
{
    public class UserHelper
    {
        public static ApplicationUser ToApplicationUser(UserRegister dto)
        {
            return new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = dto.UserName,
                NormalizedUserName = dto.UserName.ToUpper(),
                Email = dto.Email,
                NormalizedEmail = dto.Email.ToUpper(),
                FullName = dto.FullName,
                CreatedAt = DateTime.Now,
                CreatedBy = dto.UserName
            };
        }
    }
}
