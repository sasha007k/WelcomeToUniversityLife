using Application.IServices;
using Application.Models.SiteAdmin;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SiteAdminService : ISiteAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly ICampaignService _campaignService;

        public SiteAdminService(UserManager<User> userManager, IUnitOfWork unitOfWork, ICampaignService campaignService)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _campaignService = campaignService;
        }

        public async Task<bool> AddUniversityAsync(AddUniversityModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) await _userManager.AddToRoleAsync(user, "UniversityAdmin");

            var university = new University
            {
                Name = model.UniversityName,
                UserId = user.Id
            };

            await _unitOfWork.UniversityRepository.CreateAsync(university);
            var saveResult = await _unitOfWork.Commit();

            return saveResult == 1;
        }

        public List<University> GetAllUniversities()
        {
            return _unitOfWork.UniversityRepository.GetAllUniversitities().Result;
        }

        private async Task<Tuple<string, bool>> ValidateCampaign(CampaignModel model)
        {
            var startDate = model.Start;
            var endDate = model.End;
            var message = string.Empty;

            if (DateTime.Today > startDate || DateTime.Today > endDate)
            {
                message = "Start date and end date should be greater than today date.";
                return new Tuple<string, bool>(message, false);
            }

            if (startDate > endDate)
            {
                message = "Start date should be less than end date.";
                return new Tuple<string, bool>(message, false);
            }

            var campaigns = await _unitOfWork.CampaignRepository.GetAllAsync();
            if (campaigns.Any(campaign => (campaign.Start > startDate && campaign.End < startDate) || (campaign.Start < endDate && campaign.End > endDate)))
            {
                message = "Campaigns should not intersect.";
                return new Tuple<string, bool>(message, false);
            }

            return new Tuple<string, bool>(message, true);
        }

        public async Task<(string,Сampaign)> CreateCampaignAsync(CampaignModel requestData)
        {
            var (message, result) = await ValidateCampaign(requestData);
            if (!result)
            {
                return (message,null);
            }

            var newcampaign = new Сampaign
            {
                Start = requestData.Start,
                End = requestData.End,
                Status = CampaignStatus.Pending
            };
            

            await _unitOfWork.CampaignRepository.CreateAsync(newcampaign);

            await _unitOfWork.Commit();

            return (string.Empty, newcampaign);
        }

        public async Task<List<Сampaign>> GetAllCampaigns()
        {
            await _campaignService.UpdateCampaignsStatus();
            var campaigns = await _unitOfWork.CampaignRepository.GetAllAsync();

            return campaigns.ToList();
        }

        public async Task<bool> DeleteCampaignAsync(int campaignId)
        {
            await _unitOfWork.CampaignRepository.DeleteAsync(campaignId);
            return (await _unitOfWork.Commit()) == 1;
        }
    }
}