using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ApplicationRepository : Repository<Request, int>, IApplicationRepository
    {
        public ApplicationRepository(DatabaseContext context) : base(context)
        {
        }

        public Task<List<Request>> GetAllApplicationsByUserId(int userId)
        {
            return _context.Applications
                .Where(a => a.UserId == userId).ToListAsync();
        }
    }
}