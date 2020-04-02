using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(40);
            builder.HasOne(z => z.ZNO)
                .WithOne(u => u.User)
                .HasForeignKey<User>(u => u.ZNOId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
