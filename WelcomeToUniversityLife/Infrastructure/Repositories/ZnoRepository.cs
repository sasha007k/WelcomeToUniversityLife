using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class ZnoRepository : Repository<ZNO, int>, IZNORepository
    {
        public ZnoRepository(DatabaseContext context) : base(context)
        {
        }
    }
}