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

        public AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<Tuple<SignInResult, string>> SignIn(SignInModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
            var user = await _userManager.FindByNameAsync(model.Email);
            var role = await _userManager.GetRolesAsync(user);
            return new Tuple<SignInResult, string>(result, role[0]);
        }

        public async Task<IdentityResult> Register(RegisterModel model)
        {
            var user = new User {Email = model.Email, UserName = model.Email};

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
