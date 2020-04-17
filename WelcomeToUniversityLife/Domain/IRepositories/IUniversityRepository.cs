using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUniversityRepository : IRepository<University, int>
    {
        Task<University> GetUniversityWithUser(int universityId);
        Task<List<University>> GetAllUniversitities();
        Task<University> GetUniversityWithUserId(int userId);
        Task<University> GetUniversityWithId(int universityId);
    }
}