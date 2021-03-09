using ElectronicShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Configurations
{
    class AspNetUserLoginConfiguration : IEntityTypeConfiguration<AspNetUserLogin>
    {
        public void Configure(EntityTypeBuilder<AspNetUserLogin> builder)
        {
            builder.ToTable("AspNetUserLogins");

            builder.HasKey(x => new { x.LoginProvider, x.ProviderKey });

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.AspNetUserLogins)
                .HasForeignKey(x => x.UserId);
        }
    }
}
