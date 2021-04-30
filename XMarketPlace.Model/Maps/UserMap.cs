using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Map;
using XMarketPlace.Model.Entities;

namespace XMarketPlace.Model.Maps
{
    public class UserMap : CoreMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.LastName).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.Title).HasMaxLength(100).IsRequired(true);
            builder.Property(x => x.ImageUrl).HasMaxLength(255).IsRequired(false);

            builder.Property(x => x.Address).IsRequired(true);
            builder.Property(x => x.PhoneNumber).IsRequired(true);

            builder.Property(x => x.EmailAddress).IsRequired(true);
            builder.Property(x => x.Password).HasMaxLength(50).IsRequired(true);
            builder.Property(x => x.LastLogin).IsRequired(false);

            base.Configure(builder);
        }
    }
}
