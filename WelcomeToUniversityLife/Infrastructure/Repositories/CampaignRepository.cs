using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CampaignRepository : Repository<Сampaign, int>, ICampaignRepository
    {
        public CampaignRepository(DatabaseContext context) : base(context)
        {

        }

        public Task<List<Сampaign>> GetCampaignsByYearAndNotClosed(int year)
        {
            return this._context.Campaigns
                .Where(c => c.Start.Year == year && c.Status != CampaignStatus.Closed)
                .ToListAsync();
        }

        public Task<Сampaign> GetTheNearestOrCurrentCampaign()
        {
            return _context.Campaigns
                .Where(c => c.Status != CampaignStatus.Closed)
                .OrderBy(c => c.Start)
                .FirstOrDefaultAsync();
        }
    }
}
