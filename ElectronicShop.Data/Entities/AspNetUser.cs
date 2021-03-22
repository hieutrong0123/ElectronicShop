using ElectronicShop.Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ElectronicShop.Data.Entities
{
    public class AspNetUser : IdentityUser<int>
    {
        public string FirstMiddleName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public UserStatus Status { get; set; }

        public Gender Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public int CreatedBy { get; set; }

        public int ModifiedBy { get; set; }

        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public List<FavoriteProduct> FavoriteProducts { get; set; }

        public List<WatchedProduct> WatchedProducts { get; set; }

        public List<Comment> Comments { get; set; }

        public List<ProductReview> ProductReviews { get; set; }

        public List<Order> Orders { get; set; }

        public List<AspNetUserRole> AspNetUserRoles { get; set; }

        public List<AspNetUserClaim> AspNetUserClaims { get; set; }

        public List<AspNetUserLogin> AspNetUserLogins { get; set; }

        public List<AspNetUserToken> AspNetUserTokens { get; set; }
    }
}

