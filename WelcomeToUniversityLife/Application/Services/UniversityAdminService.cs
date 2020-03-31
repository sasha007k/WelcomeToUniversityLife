using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.IServices;
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

        public async Task<bool> EditUniversity(UniversityInfo model)
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
                    university.Name = CheckValue(university.Name, model.Name);
                    university.City = CheckValue(university.City, model.City);
                    university.Address = CheckValue(university.Address, model.Address);
                    university.Description = CheckValue(university.Description, model.Description);
                    university.Latitude = CheckValue(university.Latitude, model.Latitude);
                    university.Longitude = CheckValue(university.Longitude, model.Longitude);

                    result = await _dbContext.SaveChangesAsync();
                }

                return result == 1;
            }

            return false;
        }

        private string CheckValue(string oldValue, string newValue)
        {
            if (oldValue != newValue && !string.IsNullOrWhiteSpace(newValue))
            {
                return newValue;
            }

            return oldValue;
        }

        public async Task<CurrentUniversityAndFaculties> GetUniversity()
        {
            var userName = _httpContext.HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);

            if (user != null)
            {
                var university = await _dbContext.Universities
                    .SingleAsync(u => u.UserId == user.Id);

                var currentUniAndFaculties = new CurrentUniversityAndFaculties();

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

        public async Task<CurrentUniversityAndFaculties> GetUniversityAsync(int universityId)
        {
            var university = await _dbContext.Universities.FindAsync(universityId);

            var currentUniAndFaculties = new CurrentUniversityAndFaculties();

            if (university != null)
            {
                currentUniAndFaculties.CurrentUniversity = university;

                var faculties = _dbContext.Faculties
                    .Where(f => f.UniversityId == university.Id).ToList();

                currentUniAndFaculties.Faculties = faculties;
            }

            return currentUniAndFaculties;

            return null;
        }
    }
}
