using Application.Models.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IAuthenticationService
    {
        Task<Tuple<SignInResult, string>> SignIn(SignInModel model);
        Task SignOut();
        Task<IdentityResult> Register(RegisterModel model);
        Task<User> FindByEmailAsync(string email);
    }
}