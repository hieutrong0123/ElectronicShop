using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class AspNetRoleClaim : IdentityRoleClaim<int>
    {
        public AspNetRole AspNetRole { get; set; }
    }
}
