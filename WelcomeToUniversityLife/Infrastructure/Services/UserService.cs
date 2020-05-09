using Application.IServices;
using Application.Models.User;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IDocumentService _documentService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        private readonly ICampaignService _campaignService;

        private const int _maxApplications = 6;

        public UserService(IUserManager userManager, IHttpContextAccessor httpContext,
           IUnitOfWork unitOfWork, ICampaignService campaignService, IDocumentService documentService)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _documentService = documentService;
            _unitOfWork = unitOfWork;
            _campaignService = campaignService;
        }

        public async Task<UserProfileModel> GetUserInfo(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                var profile = new UserProfileModel
                {
                    id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Phone = user.PhoneNumber,
                    City = user.City,
                    DateOfBirth = user.DateOfBirth
                };

                return profile;
            }

            throw new Exception("User not found");
        }

        public async Task UpdateUserInfo(UserProfileModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.MiddleName = model.MiddleName;
                user.DateOfBirth = model.DateOfBirth;
                user.PhoneNumber = model.Phone;
                user.City = model.City;


                await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new Exception("Failed when updating");
            }
        }

        public async Task<bool> ChangePassword(ChangePasswordModel model)
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;

            var isOldPasswordCorrect =
                _userManager.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);

            if (isOldPasswordCorrect != PasswordVerificationResult.Success) return false;

            var newPassword = _userManager.HashPassword(user, model.NewPassword);
            user.PasswordHash = newPassword;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<IdentityResult> AddDocs(string name, Document document)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                document.User = user;
                document.UserId = user.Id;
                await _documentService.Create(document);

                user.Documents.Add(document);


                return await _userManager.UpdateAsync(user);
            }

            return null;
        }

        public async Task<DocsModel> GetDocsAsync(string name)
        {
            var user = await _unitOfWork.UserRepository.GetUserWithDocsAsync(name);

            DocsModel docs = new DocsModel
            {
                Passport = user.Documents.ElementAt(0).Name,
                Certificate = user.Documents.ElementAt(1).Name,
                Zno = user.Documents.ElementAt(2).Name,
            };

            return docs;
        }

        private async Task<Tuple<string, bool>> CheckCampaign()
        {
            await _campaignService.UpdateCampaignsStatus();
            var campaign = await _unitOfWork.CampaignRepository.GetTheNearestOrCurrentCampaign();
            var message = string.Empty;

            if (campaign == null)
            {
                message = "There is no campaign now.";
                return new Tuple<string, bool>(message, false);
            }

            if (campaign.Status == CampaignStatus.Pending)
            {
                var days = DateTime.Now - campaign.Start;
                message = $"{days.Days} left.";
                return new Tuple<string, bool>(message, false);
            }

            if (campaign.Status == CampaignStatus.Active)
            {
                return new Tuple<string, bool>(message, true);
            }

            return new Tuple<string, bool>(message, false);
        }

        public async Task<string> ApplyButtonExecuteAsync(int specialityId)
        {
            var (message, result) = await CheckCampaign();

            if (!result)
            {
                return message;
            }

            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var speciality = await _unitOfWork.SpecialityRepository.GetAsync(specialityId);

            if (user == null || speciality == null)
            {
                return string.Empty;
            }

            var allUsersApplications = _unitOfWork.ApplicationRepository.GetAllRequestsByUserId(user.Id).Result;
            if (allUsersApplications.Any(app => app.SpecialityId == specialityId))
            {
                return "You have already applied";
            }

            if (user.ZNOId == null)
            {
                return "Please, set you marks.";
            }

            var usersZno = await _unitOfWork.ZNORepository.GetAsync(user.ZNOId.Value);
            var userZNOs = usersZno.GetNotNullMarks();
            var requiredZNO = speciality.GetRequiredZNO();

            var isAllRequiredPresent = requiredZNO.All(x => userZNOs.Any(y => x == y));

            if (!isAllRequiredPresent)
            {
                return "Sorry, but you do not have required zno.";
            }

            if (_maxApplications <= user.NumberOfApplications)
            {
                return "Sorry, but you can not apply more.";
            }

            var application = new Domain.Entities.Request()
            {
                UserId = user.Id,
                SpecialityId = speciality.Id
            };

            user.NumberOfApplications += 1;
            await _userManager.UpdateAsync(user);
            await _unitOfWork.ApplicationRepository.CreateAsync(application);
            await _unitOfWork.Commit();

            var applicationsLeft = _maxApplications - user.NumberOfApplications;
            var word = applicationsLeft == 1 ? "application" : "applications";

            return $"You are successfully applied. {applicationsLeft} {word} left.";
        }

        public async Task<int> GetIdByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if (user != null)
            {
                return user.Id;
            }
            return -1;
        }
    }
}