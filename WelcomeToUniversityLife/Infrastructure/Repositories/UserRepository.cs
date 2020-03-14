using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class UserRepository : Repository<User,int>, IUserRepository
    {
        public UserRepository(DatabaseContext context) : base(context)
        {

        }
    }
}
