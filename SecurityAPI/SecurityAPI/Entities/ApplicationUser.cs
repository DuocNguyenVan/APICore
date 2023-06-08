using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Entities
{
    public class ApplicationUser: IdentityUser
    {
        public string? Picture { get; set; }
        public string? DateOfBirth { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        // Thêm các trường mong muốn khác ở đây
    }
}
