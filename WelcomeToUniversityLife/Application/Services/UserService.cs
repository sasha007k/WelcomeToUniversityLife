using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.User;

namespace Application.Services
{
    public class UserService : IUserService
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<UserProfileModel> GetUserInfo(string name)
        {
            User user = await _userManager.FindByNameAsync(name);

            if (user != null)
            {
                UserProfileModel profile = new UserProfileModel()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    MiddleName = user.MiddleName,
                    Phone = user.PhoneNumber,
                    City = user.City,
                    DateOfBirth = user.DateOfBirth
               
                    // ZNO marks
                };

                return profile;
            }
            else
            {
                throw new Exception("User not found");
            }

        }
    }
}
