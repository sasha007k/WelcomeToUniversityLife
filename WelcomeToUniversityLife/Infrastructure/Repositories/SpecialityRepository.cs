using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class SpecialityRepository:Repository<Speciality,int>,ISpecialityRepository
    {
        public SpecialityRepository(DatabaseContext context):base(context)
        {
        }
    }
}
