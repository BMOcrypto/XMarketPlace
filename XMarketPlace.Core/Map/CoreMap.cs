using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Entity;

namespace XMarketPlace.Core.Map
{
    public abstract class CoreMap<T> : IEntityTypeConfiguration<T> where T : CoreEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(x => x.ID); //ID is primary key
            builder.Property(x => x.Status).IsRequired(true);
            builder.Property(x => x.CreatedDate).IsRequired(false);
            builder.Property(x => x.CreatedComputerName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.CreatedIP).HasMaxLength(15).IsRequired(false);
            builder.Property(x => x.ModifiedDate).IsRequired(false);
            builder.Property(x => x.ModifiedComputerName).HasMaxLength(255).IsRequired(false);
            builder.Property(x => x.ModifiedIP).HasMaxLength(15).IsRequired(false);

            //throw new NotImplementedException();
        }
    }
}
