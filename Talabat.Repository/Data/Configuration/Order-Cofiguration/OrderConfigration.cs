﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data.Configuration.Order_Cofiguration
{
    public class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShippingAddress, ShippingAddress => ShippingAddress.WithOwner());

            builder.Property(o => o.Status).HasConversion(
                (OStatus) => OStatus.ToString(),
                (OStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus)
                );

            //one to many
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);

            

            builder.Property(o => o.SubTotal).HasColumnType("decimal (18,2)");
        }
    }
}
