using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ApplicationRepository : Repository<Request, int>, IApplicationRepository
    {
        public ApplicationRepository(DatabaseContext context) : base(context)
        {
        }

        public Task<List<Request>> GetAllRequestsByUserId(int userId)
        {
            return _context.Applications
                .Where(a => a.UserId == userId).ToListAsync();
        }

        public Task<List<Request>> GetAllRequestsBySpecialityId(int specialityId)
        {
            return _context.Applications
                .Where(app => app.SpecialityId == specialityId)
                .Include(app => app.User)
                    .ThenInclude(user => user.ZNO)
                .ToListAsync();
        }
    }
}