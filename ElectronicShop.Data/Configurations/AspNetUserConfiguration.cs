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

            builder.Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.FirstMiddleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Address)
                .IsRequired(false)
                .HasMaxLength(200);

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

            //builder.HasOne(x => x.Created)
            //    .WithMany(x => x.CreatedUsers)
            //    .HasForeignKey(x => x.CreatedBy)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(x => x.Modified)
            //    .WithMany(x => x.ModifiedUsers)
            //    .HasForeignKey(x => x.ModifiedBy)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CreatedBy)
                .IsRequired();

            builder.Property(x => x.ModifiedBy)
                .IsRequired();

            builder.Property(x => x.Birthday)
                .IsRequired(false)
                .HasColumnType("DateTime");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);
        }
    }
}