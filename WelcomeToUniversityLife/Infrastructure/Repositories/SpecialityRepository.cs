using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class SpecialityRepository : Repository<Speciality, int>, ISpecialityRepository
    {
        public SpecialityRepository(DatabaseContext context) : base(context)
        {
        }

        public Task<List<Speciality>> GetAllSpecialitiesWithFacultyId(int facultyId)
        {
            return _context.Specialities
                .Where(f => f.FacultyId == facultyId).ToListAsync();
        }

        public Task<List<Speciality>> SearchSpeciality(string filter)
        {
            return _context.Specialities
                .Include(s => s.Faculty)
                 .ThenInclude(f => f.University)
                .Where(s => s.Name.Contains(filter) || s.Faculty.Name.Contains(filter)
                || s.Faculty.University.Name.Contains(filter))
                .ToListAsync();
        }
    }
}