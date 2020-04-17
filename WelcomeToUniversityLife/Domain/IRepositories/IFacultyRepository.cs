using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IFacultyRepository : IRepository<Faculty, int>
    {
        Task<List<Faculty>> GetAllFacultiesWithUniversityId(int universityId);
    }
}