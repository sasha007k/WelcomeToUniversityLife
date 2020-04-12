using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.SiteAdmin;
using Domain.Entities;

namespace Application.IServices
{
    public interface ISiteAdminService
    {
        Task<bool> AddUniversityAsync(AddUniversityModel model);
        List<University> GetAllUniversities();
        Task CreateCampaignAsync(CampaignModel requestData);
        Task<List<Сampaign>> GetAllCampaigns();
        Task<bool> DeleteCampaignAsync(int campaignId);
    }
}