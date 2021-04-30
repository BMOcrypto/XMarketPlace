using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Map;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.Model.Maps
{
    public class CategoryMap : CoreMap<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.Description).HasMaxLength(255).IsRequired(true);

            base.Configure(builder);
        }
    }
}
