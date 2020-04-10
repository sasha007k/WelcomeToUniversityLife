using Domain.Entities;
using Domain.IRepositories;

namespace Infrastructure.Repositories
{
    public class ApplicationRepository : Repository<Request, int>, IApplicationRepository
    {
        public ApplicationRepository(DatabaseContext context) : base(context)
        {
        }
    }
}