using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class AspNetUserRole : IdentityUserRole<int>
    {
        public AspNetUser AspNetUser { get; set; }

        public AspNetRole AspNetRole { get; set; }
    }
}