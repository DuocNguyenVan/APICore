using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Constants
{
    public static class AppJwtClaimTypes
    {
        public const string Subject = "sub";
        public const string UserName = "username";
        public const string FullName = "full_name";
        public const string Roles = "roles";
    }
}
