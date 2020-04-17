using Application.Models.User;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserInfo(string name);
        Task UpdateUserInfo(UserProfileModel model);
        Task<bool> ChangePassword(ChangePasswordModel model);
        Task<string> ApplyButtonExecuteAsync(int specialityId);
        Task<IdentityResult> AddDocs(string name, Document document);
        Task<int> GetIdByName(string name);
    }
}