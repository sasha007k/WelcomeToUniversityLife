using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Application.Models.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

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
