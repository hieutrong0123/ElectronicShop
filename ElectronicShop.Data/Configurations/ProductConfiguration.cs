using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ElectronicShop.Data.Entities;

namespace ElectronicShop.Data.Configurations
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.Price)
                .HasColumnType("decimal(18,0)")
                .IsRequired();

            builder.Property(x => x.Specifications)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired();

            builder.Property(x => x.GoodsReceipt)
                .IsRequired();

            builder.Property(x => x.Inventory)
                .IsRequired();

            builder.Property(x => x.Status)
                .IsRequired();

            builder.Property(x => x.Alias)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(x => x.DateCreated)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.Property(x => x.DateModified)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CreatedBy).
                OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.ModifiedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
