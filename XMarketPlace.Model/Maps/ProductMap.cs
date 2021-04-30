using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Map;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.Model.Maps
{
    public class ProductMap : CoreMap<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ProductName).HasMaxLength(150).IsRequired(true);
            builder.Property(x => x.ProductDetail).IsRequired(true);
            builder.Property(x => x.ImagePath).IsRequired(false);
            builder.Property(x => x.UnitPrice).IsRequired(true);
            builder.Property(x => x.UnitsInStock).IsRequired(true);

            base.Configure(builder);
        }
    }
}
