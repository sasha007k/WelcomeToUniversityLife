using System.Collections.Generic;
using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ICampaignRepository : IRepository<Сampaign, int>
    {
        Task<List<Сampaign>> GetCampaignsByYearAndNotClosed(int year);
        Task<Сampaign> GetTheNearestOrCurrentCampaign();
    }
}
