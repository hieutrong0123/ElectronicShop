using ElectronicShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Configurations
{
    class AspNetUserConfiguration : IEntityTypeConfiguration<AspNetUser>
    {
        public void Configure(EntityTypeBuilder<AspNetUser> builder)
        {
            builder.ToTable("AspNetUsers");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.FirstMiddleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Address)
                .IsRequired(false);

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Gender)
                .IsRequired();

            builder.Property(x => x.DateCreated)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.Property(x => x.DateModified)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.Property(x => x.CreatedBy)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(x => x.ModifiedBy)
                .IsRequired(false)
                .HasMaxLength(256);

            builder.Property(x => x.Birthday)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(x => x.Email)
                .IsRequired();
        }
    }
}