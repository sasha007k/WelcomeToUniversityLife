using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FacultyRepository:Repository<Faculty,int>,IFacultyRepository
    {
        public FacultyRepository(DatabaseContext context):base(context)
        {
        }
        public Task<List<Faculty>> GetAllFacultiesWithUniversityId(int universityId)
        {
            return _context.Faculties
                .Where(f => f.UniversityId == universityId).ToListAsync();
        }
    }
}
