using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ZNOConfiguration : IEntityTypeConfiguration<ZNO>
    {
        public void Configure(EntityTypeBuilder<ZNO> builder)
        {
            builder.ToTable("ZNO");
            builder.HasKey(u => u.Id);
        }
    }
}
