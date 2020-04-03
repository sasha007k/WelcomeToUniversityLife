using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class FacultyRepository:Repository<Faculty,int>,IFacultyRepository
    {
        public FacultyRepository(DatabaseContext context):base(context)
        {
        }
    }
}
