using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
            builder.HasOne(f => f.University)
                .WithMany(u => u.Faculties)
                .HasForeignKey(f => f.UniversityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}