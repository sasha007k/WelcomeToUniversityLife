﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
    {
        public void Configure(EntityTypeBuilder<Speciality> builder)
        {
            builder.ToTable("Speciality");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(60);
            builder.Property(u => u.Description)
                .HasMaxLength(1000);
            builder.Property(u => u.PaidSpaces)
                .IsRequired();
            builder.Property(u => u.FreeSpaces)
                .IsRequired();
            builder.Property(u => u.RequiredZNO1)
                .IsRequired();
            builder.Property(u => u.RequiredZNO2)
                .IsRequired();
            builder.HasOne(s => s.Faculty)
                .WithMany(f => f.Specialities)
                .HasForeignKey(s => s.FacultyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}