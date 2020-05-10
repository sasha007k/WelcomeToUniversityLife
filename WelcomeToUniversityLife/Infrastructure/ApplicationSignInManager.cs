using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class ApplicationSignInManager : ISignInManager
    {
        public ApplicationSignInManager(SignInManager<User> signInManager)
        {
            this.SignInManager = signInManager;
        }

        public SignInManager<User> SignInManager { get; }
        public Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return SignInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }

        public Task SignOutAsync()
        {
            return SignInManager.SignOutAsync();
        }
    }
}
