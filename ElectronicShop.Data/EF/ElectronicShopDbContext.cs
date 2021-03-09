using ElectronicShop.Data.Configurations;
using ElectronicShop.Data.Entities;
using ElectronicShop.Data.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.EF
{
    public class ElectronicShopDbContext : IdentityDbContext<AspNetUser, AspNetRole, int, AspNetUserClaim, AspNetUserRole, AspNetUserLogin, AspNetRoleClaim, AspNetUserToken>
    {
        public ElectronicShopDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning You can move this code to protect potentially senstive information
                //in connection string.

                optionsBuilder.UseSqlServer("Server=DESKTOP-MUM11D5\\SQLEXPRESS;Database=ElectronicShopDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new AspNetRoleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUserClaimConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUserConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUserLoginConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AspNetUserTokenConfiguration());

            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CommentConfiguration());
            modelBuilder.ApplyConfiguration(new FavoriteProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusConfiguration());
            modelBuilder.ApplyConfiguration(new OrderStatusDetailConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductPhotoConfiguration());
            modelBuilder.ApplyConfiguration(new ProductReviewConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new WatchedProductConfiguration());

            //Data Seeding
            modelBuilder.Seed();
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderStatus> OrderStatuses { get; set; }

        public DbSet<OrderStatusDetail> OrderStatusDetails { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductPhoto> ProductPhotos { get; set; }

        public DbSet<ProductReview> ProductReviews { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<WatchedProduct> WatchedProducts { get; set; }


    }
}
