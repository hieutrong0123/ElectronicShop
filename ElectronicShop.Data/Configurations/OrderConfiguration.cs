using ElectronicShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ElectronicShop.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseIdentityColumn();

            builder.Property(x => x.DateCreated)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.Property(x => x.DeliveryDate)
                .HasColumnType("DateTime")
                .HasDefaultValueSql("GetDate()");

            builder.Property(x => x.Paid)
                .IsRequired();

            builder.Property(x => x.Receiver)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(x => x.ReceiversAddress)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNumber)
                .IsRequired();

            builder.Property(x => x.TotalMoney)
                .IsRequired()
                .HasColumnType("decimal(18,10)");

            builder.HasOne(x => x.OrderStatus)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.AspNetUser)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
