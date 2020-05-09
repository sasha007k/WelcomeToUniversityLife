using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.IServices
{
    public interface IUserManager
    {
        Task<IdentityResult> CreateAsync(User user, string password);
        Task<IdentityResult> AddToRoleAsync(User user, string role);  
        Task<User> FindByNameAsync(string name);
        Task<User> FindByEmailAsync(string email);
        Task<IList<string>> GetRolesAsync(User user);
        Task<IdentityResult> UpdateAsync(User user);
        PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword);
        string HashPassword(User user, string password);
    }
}
