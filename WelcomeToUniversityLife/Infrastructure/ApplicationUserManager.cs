using System.Collections.Generic;
using System.Threading.Tasks;
using Application.IServices;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    public class ApplicationUserManager :  IUserManager
    {
        public ApplicationUserManager(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        public UserManager<User> UserManager { get; }

        public Task<IdentityResult> AddToRoleAsync(User user, string role)
        {
            return UserManager.AddToRoleAsync(user, role);
        }

        public Task<User> FindByNameAsync(string name)
        {
            return UserManager.FindByNameAsync(name);
        }

        public Task<IdentityResult> CreateAsync(User user, string password)
        {
            return UserManager.CreateAsync(user, password);
        }

        public Task<User> FindByEmailAsync(string email)
        {
            return UserManager.FindByEmailAsync(email);
        }

        public Task<IList<string>> GetRolesAsync(User user)
        {
            return UserManager.GetRolesAsync(user);
        }

        public Task<IdentityResult> UpdateAsync(User user)
        {
            return UserManager.UpdateAsync(user);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            return UserManager.PasswordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword);
        }

        public string HashPassword(User user, string password)
        {
            return UserManager.PasswordHasher.HashPassword(user, password);
        }
    }
}
