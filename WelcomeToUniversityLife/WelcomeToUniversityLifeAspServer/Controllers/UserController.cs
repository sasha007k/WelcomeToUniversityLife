using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using Application.Models.User;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class UserController : Controller
    {
        IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            UserProfileModel profile = await _userService.GetUserInfo(User.Identity.Name).ConfigureAwait(true);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> PersonalInfo(UserProfileModel model)
        {
            await _userService.UpdateUserInfo(model).ConfigureAwait(true);

            return RedirectToAction("Profile", "User");
        }

    }
}
