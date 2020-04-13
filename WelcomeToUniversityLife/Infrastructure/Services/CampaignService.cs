using System;
using System.Threading.Tasks;
using Application.IServices;
using Domain;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CampaignService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateCampaignsStatus()
        {
            var campaigns = await _unitOfWork.CampaignRepository.GetCampaignsByYearAndNotClosed(DateTime.Today.Year);
            if (campaigns != null)
            {
                foreach (var campaign in campaigns)
                {
                    switch (campaign.Status)
                    {
                        case CampaignStatus.Pending:
                        {
                            if (campaign.Start < DateTime.Now)
                            {
                                campaign.Status = CampaignStatus.Active;
                                await _unitOfWork.Commit();
                            }

                            break;
                        }
                        case CampaignStatus.Active:
                        {
                            if (campaign.End < DateTime.Now)
                            {
                                campaign.Status = CampaignStatus.Closed;
                                await _unitOfWork.Commit();
                            }

                            break;
                        }
                    }
                }
            }
        }
    }
}
