using System.Threading.Tasks;
using Application.Models.User;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Application.IServices
{
    public interface IUserService
    {
        Task<UserProfileModel> GetUserInfo(string name);
        Task UpdateUserInfo(UserProfileModel model);
        Task<bool> ChangePassword(ChangePasswordModel model);
        Task<IdentityResult> AddDocs(string name, Document document);
        Task<bool> ApplyButtonExecuteAsync(int specialityId);
        Task<int> GetIdByName(string name);
    }
}