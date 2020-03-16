using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.User;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserInfo(string name);
        Task UpdateUserInfo(UserProfileModel model);
    }
}
