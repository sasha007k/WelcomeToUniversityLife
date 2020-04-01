using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.User;
using Microsoft.AspNetCore.Http;
namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserInfo(string name);
        Task UpdateUserInfo(UserProfileModel model);
        Task<bool> ChangePassword(ChangePasswordModel model);
        Task AddDocs(string name, IFormFileCollection uploads);
    }
}
