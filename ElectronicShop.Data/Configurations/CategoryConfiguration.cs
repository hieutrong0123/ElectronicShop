﻿using ElectronicShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.Alias)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.DateCreated)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.Property(x => x.DateModified)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict); ;

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.RootId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.ProductType)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.ProductTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
