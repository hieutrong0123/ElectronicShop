using ElectronicShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Configurations
{
    class AspNetUserTokenConfiguration : IEntityTypeConfiguration<AspNetUserToken>
    {
        public void Configure(EntityTypeBuilder<AspNetUserToken> builder)
        {
            builder.ToTable("AspNetUserTokens");

            builder.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.AspNetUserTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}
