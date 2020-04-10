using System.Collections.Generic;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.SiteAdmin;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class SiteAdminService : ISiteAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public SiteAdminService(UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
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
    }
}