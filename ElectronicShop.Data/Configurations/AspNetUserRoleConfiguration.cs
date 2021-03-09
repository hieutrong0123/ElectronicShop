using ElectronicShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Configurations
{
    class AspNetUserRoleConfiguration : IEntityTypeConfiguration<AspNetUserRole>
    {
        public void Configure(EntityTypeBuilder<AspNetUserRole> builder)
        {
            builder.ToTable("AspNetUserRoles");

            builder.HasKey(x => new { x.RoleId, x.UserId });

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.AspNetUserRoles)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.AspNetRole)
                .WithMany(x => x.AspNetUserRoles)
                .HasForeignKey(x => x.RoleId);
        }
    }
}
