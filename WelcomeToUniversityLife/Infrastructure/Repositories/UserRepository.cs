using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {
        }

        public async Task<User> GetUserWithUniversityAsync(int userId)
        {
            return await _context.Users
                .Where(user => user.Id == userId)
                .Include(user => user.University)
                .FirstOrDefaultAsync();
        }
    }
}