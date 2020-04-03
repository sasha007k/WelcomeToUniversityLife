using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class ApplicationConfiguration: IEntityTypeConfiguration<Request>
    {
        public void Configure(EntityTypeBuilder<Request> builder)
        {
            builder.ToTable("Application");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.UserId)
                .IsRequired();
            builder.Property(u => u.SpecialityId)
                .IsRequired();
        }
    }
}
