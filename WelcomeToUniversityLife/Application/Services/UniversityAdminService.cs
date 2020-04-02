using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.Enum;
using Application.Models.UniversityAdmin;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UniversityAdminService : IUniversityAdminService
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        DatabaseContext _dbContext;
        IHttpContextAccessor _httpContext;

        public UniversityAdminService(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext dbContext, IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _httpContext = httpContext;
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
                    university.Latitude = model.Latitude;
                    university.Longitude =  model.Longitude;

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
    }
}
