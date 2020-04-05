using System;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.Enum;
using Application.Models.UniversityAdmin;
using Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services
{
    public class UniversityAdminService : IUniversityAdminService
    {
        UserManager<User> _userManager;
        DatabaseContext _dbContext;
        IHttpContextAccessor _httpContext;
        private readonly IUnitOfWork _unit;
        private readonly IPhotoHelper _photoHelper;

        public UniversityAdminService(UserManager<User> userManager,IUnitOfWork unit, 
            DatabaseContext dbContext, IHttpContextAccessor httpContext,IPhotoHelper photoHelper)
        {
            _userManager = userManager;
            _dbContext = dbContext;
            _httpContext = httpContext;
            _unit = unit;
            _photoHelper = photoHelper;
        }

        public async Task<bool> EditUniversity(UniversityInfoModel model)
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var university = _dbContext.Universities
                    .Single(u => u.UserId == user.Id);

                var result = 0;

                if (university != null)
                {
                    university.Name = model.Name;
                    university.City = model.City;
                    university.Address =  model.Address;
                    university.Description = model.Description;
                    university.LocationLink = model.LocationLink;

                    result = await _dbContext.SaveChangesAsync();
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
                var university = _dbContext.Universities
                    .Single(u => u.UserId == user.Id);

                var currentUniAndFaculties = new CurrentUniversityAndFacultiesModel();

                if (university != null)
                {
                    currentUniAndFaculties.CurrentUniversity = university;

                    var faculties = _dbContext.Faculties
                        .Where(f => f.UniversityId == university.Id).ToList();

                    currentUniAndFaculties.Faculties = faculties;
                }

                return currentUniAndFaculties;
            }

            return null;
        }

        public async Task<CurrentUniversityAndFacultiesModel> GetUniversityAsync(int universityId)
        {
            var university = await _dbContext.Universities.FindAsync(universityId);

            var currentUniAndFaculties = new CurrentUniversityAndFacultiesModel();

            if (university != null)
            {
                currentUniAndFaculties.CurrentUniversity = university;

                var faculties = _dbContext.Faculties
                    .Where(f => f.UniversityId == university.Id).ToList();

                currentUniAndFaculties.Faculties = faculties;

                return currentUniAndFaculties;
            }

            return null;
        }

        public async Task<bool> AddFacultyAsync(AddFacultyModel model)
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var university = _dbContext.Universities
                    .Single(u => u.UserId == user.Id);

                var faculty = new Faculty()
                {
                    Name = model.FacultyName,
                    Address = model.Address,
                    Description = model.Description,
                    UniversityId = university.Id
                };

                await _dbContext.Faculties.AddAsync(faculty);
                var result = await _dbContext.SaveChangesAsync();

                return result == 1;
            }

            return false;
        }

        public async Task<CurrentFacultyAndSpecialitiesModel> GetFacultyAsync(int facultyId)
        {
            var faculty = await _dbContext.Faculties.FindAsync(facultyId);

            var currentFacultyAndSpecialities = new CurrentFacultyAndSpecialitiesModel();

            if (faculty != null)
            {
                currentFacultyAndSpecialities.CurrentFaculty = faculty;

                var specialities = _dbContext.Specialities
                    .Where(f => f.FacultyId == faculty.Id).ToList();

                currentFacultyAndSpecialities.Specialities = specialities;

                var university = await _dbContext.Universities.FirstOrDefaultAsync(uni => uni.Id == faculty.UniversityId);

                currentFacultyAndSpecialities.FacultyAdminId = university.UserId;

                return currentFacultyAndSpecialities;
            }

            return null;
        }

        public async Task<bool> AddSpecialityAsync(AddSpecialityModel model)
        {
            var speciality = new Speciality()
            {
                Description = model.Description,
                Name = model.SpecialityName,
                FacultyId = model.FacultyId,
                FreeSpaces = model.FreeSpaces,
                PaidSpaces = model.PaidSpaces,
                RequiredZNO1 = AllZNO.GetZNOName(ZNOs.Ukrainian)
            };

            switch (model.ZNO.Count)
            {
                case 1:
                    speciality.RequiredZNO2 = model.ZNO[0];
                    break;
                case 2:
                    speciality.RequiredZNO2 = model.ZNO[0];
                    speciality.RequiredZNO3 = model.ZNO[1];
                    break;
            }

            await _dbContext.Specialities.AddAsync(speciality);
            var result = await _dbContext.SaveChangesAsync();

            return result == 1;
        }

        public async Task UploadUniversityPhotoAsync(UploadPhotoModel requestedData, IFormFileCollection uploads)
        {
            if (uploads.Count == 0)
                throw new Exception("File was not given!!");

            var university = await _unit.UniversityRepository.GetUniversityWityUser(requestedData.id);

            if (university == null || university.UserId != requestedData.requestedUserId)
                throw new Exception("Invalid Data!!");

            var photoName = await _photoHelper.UploadPhotoAsync(uploads[0], "universitiesphotos");

            university.Photo = photoName;

            await _unit.Commit();
        }
    }
}
