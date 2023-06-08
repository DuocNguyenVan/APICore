using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Entities
{
    public class ApplicationRole: IdentityRole
    {
        public string? Description { get; set; }
        // Thêm các trường mong muốn khác ở đây
    }
}
