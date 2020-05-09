using Application.IServices;
using Application.Models.UniversityAdmin;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly IPhotoHelperService _photoHelperService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;

        public UniversityService(IUserManager userManager, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContext, IPhotoHelperService photoHelperService)
        {
            _userManager = userManager;
            _httpContext = httpContext;
            _unitOfWork = unitOfWork;
            _photoHelperService = photoHelperService;
        }

        public async Task<bool> EditUniversity(University model)
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var university = _unitOfWork.UniversityRepository.GetUniversityWithUserId(user.Id).Result;

                var result = 0;

                if (university != null)
                {
                    university.Name = model.Name;
                    university.City = model.City;
                    university.Address = model.Address;
                    university.Description = model.Description;
                    university.LocationLink = model.LocationLink;

                    result = await _unitOfWork.Commit();
                }

                return result == 1;
            }

            return false;
        }

        public async Task<CurrentUniversityAndFacultiesModel> GetUniversity()
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var university = _unitOfWork.UniversityRepository.GetUniversityWithUserId(user.Id).Result;

                var currentUniAndFaculties = new CurrentUniversityAndFacultiesModel();

                if (university != null)
                {
                    currentUniAndFaculties.CurrentUniversity = university;

                    var faculties = _unitOfWork.FacultyRepository.GetAllFacultiesWithUniversityId(university.Id).Result;

                    currentUniAndFaculties.Faculties = faculties;
                }

                return currentUniAndFaculties;
            }

            return null;
        }

        public async Task<CurrentUniversityAndFacultiesModel> GetUniversityAsync(int universityId)
        {
            var university = await _unitOfWork.UniversityRepository.GetAsync(universityId);

            var currentUniAndFaculties = new CurrentUniversityAndFacultiesModel();

            if (university != null)
            {
                currentUniAndFaculties.CurrentUniversity = university;

                var faculties = _unitOfWork.FacultyRepository.GetAllFacultiesWithUniversityId(universityId).Result;

                currentUniAndFaculties.Faculties = faculties;

                return currentUniAndFaculties;
            }

            return null;
        }

        public async Task UploadUniversityPhotoAsync(UploadPhotoModel requestedData, IFormFileCollection uploads)
        {
            if (uploads.Count == 0)
                throw new Exception("File was not given!!");

            var university = await _unitOfWork.UniversityRepository.GetUniversityWithUser(requestedData.id);

            if (university == null || university.UserId != requestedData.requestedUserId)
                throw new Exception("Invalid Data!!");

            var photoName = await _photoHelperService.UploadPhotoAsync(uploads[0], "universitiesphotos");

            university.Photo = photoName;

            await _unitOfWork.Commit();
        }

        public async Task DeleteUniversityPhotoAsync(int userId)
        {
            var user = await _unitOfWork.UserRepository.GetUserWithUniversityAsync(userId);

            if (user.University == null)
                throw new Exception("User is not university admin!!");

            _photoHelperService.DeletePhotoAsync(user.University.Photo, "universitiesphotos");

            user.University.Photo = string.Empty;

            await _unitOfWork.Commit();
        }
    }
}