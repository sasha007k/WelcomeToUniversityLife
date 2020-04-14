using System;
using System.Threading.Tasks;
using Application.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserInfo(string name);
        Task UpdateUserInfo(UserProfileModel model);
        Task<bool> ChangePassword(ChangePasswordModel model);
        Task<IdentityResult> AddDocs(string name, IFormFileCollection uploads);
        Task<string> ApplyButtonExecuteAsync(int specialityId);
    }
}