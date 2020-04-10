using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IFacultyRepository : IRepository<Faculty, int>
    {
        Task<List<Faculty>> GetAllFacultiesWithUniversityId(int universityId);
    }
}