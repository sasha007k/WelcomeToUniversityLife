using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class SpecialityRepository:Repository<Speciality,int>,ISpecialityRepository
    {
        public SpecialityRepository(DatabaseContext context):base(context)
        {
        }
        public Task<List<Speciality>> GetAllSpecialitiesWithFacultyId(int facultyId)
        {
            return _context.Specialities
                .Where(f => f.FacultyId == facultyId).ToListAsync();
        }
    }
}
