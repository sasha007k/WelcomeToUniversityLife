using Domain.Entities;
using Domain.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CampaignRepository : Repository<Сampaign, int> ,ICampaignRepository
    {
        public CampaignRepository(DatabaseContext context) : base(context)
        {

        }

        public Task<Сampaign> GetCampaignByYear(int year)
        {
            return this._context.Campaigns
                .Where(c =>c.Start.Year == year)
                .FirstOrDefaultAsync();
        }
    }
}
