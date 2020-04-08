using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetUserWithUniversityAsync(int userId);
    }
}
