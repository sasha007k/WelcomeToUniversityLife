using Domain.Entities;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface ICampaignRepository : IRepository<Сampaign, int>
    {
        Task<Сampaign> GetCampaignByYear(int year);
    }
}
