using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppUser : IdentityUser<Int64>
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
        public string OrgUniqueCode { get; set; } = "";
    }
}
