using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.IRepositories
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<User> GetUserWithUniversityAsync(int userId);
    }
}