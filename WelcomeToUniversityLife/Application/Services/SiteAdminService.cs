using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.SiteAdmin;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class SiteAdminService : ISiteAdminService
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        DatabaseContext _dbContext;

        public SiteAdminService(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task<bool> AddUniversityAsync(AddUniversityModel model)
        {
            var user = new User
            {
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "UniversityAdmin");
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            }

            var university = new University()
            {
                Name = model.UniversityName,
                UserId = user.Id
            };

            await _dbContext.Universities.AddAsync(university);
            var saveResult = await _dbContext.SaveChangesAsync();

            return saveResult == 1;
        }
    }
}
