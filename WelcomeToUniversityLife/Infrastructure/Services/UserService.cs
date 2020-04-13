using System;
using System.IO;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.User;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly DocumentService _documentService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager, IHttpContextAccessor httpContext,
            DatabaseContext context, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _documentService = new DocumentService(_unitOfWork);
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfileModel> GetUserInfo(string name)
        {
            var user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                var profile = new UserProfileModel
                {
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
                _userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, model.OldPassword);

            if (isOldPasswordCorrect != PasswordVerificationResult.Success) return false;

            var newPassword = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            user.PasswordHash = newPassword;
            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<IdentityResult> AddDocs(string name, Document document)
        {
            var user = await _userManager.FindByNameAsync(name);
      
 
            if (user != null)
            {
                await _documentService.Create(document);
    
                user.Documents.Add(document);


                return await _userManager.UpdateAsync(user);
            }

            return null;
        }

        public async Task<bool> ApplyButtonExecuteAsync(int specialityId)
        {
            //var userName = _httpContext.HttpContext.User.Identity.Name;
            //var user = await _userManager.FindByNameAsync(userName);

            //var speciality = await _dbContext.Specialities.FindAsync(specialityId);

            //if (user != null && speciality != null)
            //{
            //    var userZNO = new Lazy<string>();

            //        // bool existsCheck = list1.All(x => list2.Any(y => x.SupplierId == y.SupplierId));

            //    var application = new Domain.Entities.Request()
            //    {
            //        UserId = user.Id,
            //        SpecialityId = speciality.Id
            //    };

            //}

            return true;
        }

        public Task<User> GetUserByIdAsync(int id)
        {
            return _unitOfWork.UserRepository.GetAsync(id);
        }

        public async Task<int> GetIdByName(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            if(user!=null)
            {
                return user.Id;
            }
            return -1;
        }
    }
}