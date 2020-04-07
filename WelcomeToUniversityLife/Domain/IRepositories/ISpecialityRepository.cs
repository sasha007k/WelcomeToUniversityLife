using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface ISpecialityRepository:IRepository<Speciality,int>
    {
        Task<List<Speciality>> GetAllSpecialitiesWithFacultyId(int facultyId);
    }
}
