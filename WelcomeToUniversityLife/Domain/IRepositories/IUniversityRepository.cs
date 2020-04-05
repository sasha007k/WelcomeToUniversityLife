using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUniversityRepository:IRepository<University,int>
    {
        Task<University> GetUniversityWityUser(int universityId);
    }
}
