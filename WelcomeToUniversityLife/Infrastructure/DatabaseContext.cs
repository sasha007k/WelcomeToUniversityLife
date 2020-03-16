using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Domain.Entities;
using Infrastructure.Configurations;

namespace Infrastructure
{
    public class DatabaseContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public DbSet<Document> Documents { get; set; }

        public DbSet<University> Universities { get; set; }

        public DbSet<Speciality> Specialities { get; set; }

        //public DbSet<ZNO> ZNOs { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new DocumentConfiguration());
            builder.ApplyConfiguration(new SpecialityConfiguration());
            builder.ApplyConfiguration(new UniversityConfiguration());
            //builder.ApplyConfiguration(new ZNOConfiguration());
        }
    }
}
