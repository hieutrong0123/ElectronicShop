using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class AspNetUserLogin : IdentityUserLogin<int>
    {
        public AspNetUser AspNetUser { get; set; }
    }
}
