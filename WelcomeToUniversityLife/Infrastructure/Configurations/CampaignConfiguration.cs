using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Сampaign>
    {
        public void Configure(EntityTypeBuilder<Сampaign> builder)
        {
            builder.ToTable("Campaigns");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Start)
                .IsRequired();
            builder.Property(u => u.End)
                .IsRequired();
            builder.Property(u => u.Status)
                .IsRequired();
        }
    }
}