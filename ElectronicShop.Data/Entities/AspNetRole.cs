using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class AspNetRole : IdentityRole<int>
    {
        public List<AspNetUserRole> AspNetUserRoles { get; set; }

        public List<AspNetRoleClaim> AspNetRoleClaims { get; set; }
    }
}
