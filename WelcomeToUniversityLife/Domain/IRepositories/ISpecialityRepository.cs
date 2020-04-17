using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ISpecialityRepository : IRepository<Speciality, int>
    {
        Task<List<Speciality>> GetAllSpecialitiesWithFacultyId(int facultyId);

        Task<List<Speciality>> SearchSpeciality(string filter);
    }
}