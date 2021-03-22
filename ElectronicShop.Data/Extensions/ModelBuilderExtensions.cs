using ElectronicShop.Data.Entities;
using ElectronicShop.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<AspNetUser>();
            modelBuilder.Entity<AspNetUser>().HasData(
                new AspNetUser
                {
                    Id = 1,
                    UserName = "hieunguyen",
                    PasswordHash = hasher.HashPassword(null, "User123456a@"),
                    FirstMiddleName = "Nguyễn Trung",
                    LastName = "Hiếu",
                    Address = "KTX Cỏ May, khu phố 6, phường Linh Trung, quận Thủ Đức, TP.HCM",
                    Status = 0,
                    Gender = Gender.MALE,
                    Email = "hieutanmy321@gmail.com",
                    PhoneNumber = "0965924083",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    NormalizedUserName = "HIEUNGUYEN",
                    NormalizedEmail = "HIEUTANMY321@GMAIL.COM",
                    CreatedBy = 1,
                    ModifiedBy = 1
                },
                new AspNetUser
                {
                    Id = 2,
                    UserName = "hieuvo",
                    PasswordHash = hasher.HashPassword(null, "User123456a@"),
                    FirstMiddleName = "Võ Trọng",
                    LastName = "Hiếu",
                    Address = "KTX Cỏ May, khu phố 6, phường Linh Trung, quận Thủ Đức, TP.HCM",
                    Status = 0,
                    Gender = Gender.MALE,
                    Email = "hieu@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    NormalizedUserName = "HIEUVO",
                    NormalizedEmail = "HIEU@GMAIL.COM",
                    CreatedBy = 1,
                    ModifiedBy = 1
                },
                new AspNetUser
                {
                    Id = 3,
                    UserName = "datle",
                    PasswordHash = hasher.HashPassword(null, "User123456a@"),
                    FirstMiddleName = "Lê Tấn",
                    LastName = "Đạt",
                    Address = "KTX Cỏ May, khu phố 6, phường Linh Trung, quận Thủ Đức, TP.HCM",
                    Status = 0,
                    Gender = Gender.MALE,
                    Email = "dat@gmail.com",
                    SecurityStamp = Guid.NewGuid().ToString(),
                    NormalizedUserName = "DATLE",
                    NormalizedEmail = "DAT@GMAIL.COM",
                    CreatedBy = 1,
                    ModifiedBy = 1
                }
                );

            modelBuilder.Entity<AspNetRole>().HasData(
                new AspNetRole
                {
                    Id = 1,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new AspNetRole
                {
                    Id = 2,
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE"
                },
                new AspNetRole
                {
                    Id = 3,
                    Name = "User",
                    NormalizedName = "USER"
                }
            );

            modelBuilder.Entity<AspNetUserRole>().HasData(
                new AspNetUserRole
                {
                    UserId = 1,
                    RoleId = 1
                },
                new AspNetUserRole
                {
                    UserId = 2,
                    RoleId = 2
                },
                new AspNetUserRole
                {
                    UserId = 3,
                    RoleId = 3
                }
            );

            modelBuilder.Entity<ProductType>().HasData(
                new ProductType
                {
                    Id = 1,
                    Name = "Laptop - Thiết bị IT",
                    Status = true
                },
                new ProductType
                {
                    Id = 2,
                    Name = "Điện Thoại - Máy tính bảng",
                    Status = true
                },
               new ProductType
               {
                   Id = 3,
                   Name = "Máy ảnh - Quay phim",
                   Status = true
               },
                new ProductType
                {
                    Id = 4,
                    Name = "Điện tử - Điện lạnh",
                    Status = true
                }
            );

            modelBuilder.Entity<OrderStatus>().HasData(
                new OrderStatus
                {
                    Id = 1,
                    Name = "Đặt hàng thành công"
                },
                new OrderStatus
                {
                    Id = 2,
                    Name = "Đã tiếp nhận"
                },
                new OrderStatus
                {
                    Id = 3,
                    Name = "Đang lấy hàng"
                },
                new OrderStatus
                {
                    Id = 4,
                    Name = "Đóng gói xong"
                },
                new OrderStatus
                {
                    Id = 5,
                    Name = "Bàn giao vận chuyển"
                },
                new OrderStatus
                {
                    Id = 6,
                    Name = "Đang vận chuyển"
                },
                new OrderStatus
                {
                    Id = 7,
                    Name = "Giao hàng thành công"
                },
                new OrderStatus
                {
                    Id = 8,
                    Name = "Hủy đơn hàng"
                }
            );
        }

    }
}
