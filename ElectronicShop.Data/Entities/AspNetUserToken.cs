using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Entities
{
    public class AspNetUserToken : IdentityUserToken<int>
    {
        public AspNetUser AspNetUser { get; set; }
    }
}
