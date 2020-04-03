using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class UniversityRepository:Repository<University,int>,IUniversityRepository
    {
        public UniversityRepository(DatabaseContext context):base(context)
        {
        }
    }
}
