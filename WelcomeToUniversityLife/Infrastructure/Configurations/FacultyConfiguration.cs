﻿using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable("Faculty");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired();
            builder.Property(u => u.Address)
                .IsRequired();
            builder.Property(u => u.DocumentId)
                .IsRequired();
            builder.Property(u => u.UniversityId)
                .IsRequired();
        }
    }
}
