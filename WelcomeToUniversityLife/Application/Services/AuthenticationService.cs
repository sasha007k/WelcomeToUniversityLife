using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.Authentication;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        DatabaseContext _dbContext;

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, DatabaseContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<SignInResult> SignIn(SignInModel model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {  
            var user = new User();
            user.Email = model.Email;
            user.UserName = model.Email;

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            }

            return result;
        }

        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }
    }
}
