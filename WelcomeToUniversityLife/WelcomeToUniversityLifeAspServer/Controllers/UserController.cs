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
        IZnoService _znoService;

        public UserController(IUserService userService, IZnoService znoService)
        {
            _userService = userService;
            _znoService = znoService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile = await _userService.GetUserInfo(User.Identity.Name).ConfigureAwait(true);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> PersonalInfo(UserProfileModel model)
        {
            await _userService.UpdateUserInfo(model).ConfigureAwait(true);

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserProfileModel model)
        {
            if (model.ChangePasswordModel != null && model.ChangePasswordModel.NewPassword == model.ChangePasswordModel.ConfirmNewPassword)
            {
                await _userService.ChangePassword(model.ChangePasswordModel).ConfigureAwait(true);
            }

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> AddDocs(IFormFileCollection uploads)
        {
            if(uploads != null)
            {
                var result = await _userService.AddDocs(User.Identity.Name,uploads);
                if (result.Succeeded)
                {
                    return RedirectToAction("Profile", "User");
                }
            }

            return BadRequest("Can't save documents");
        }

        [HttpPost]
        public async Task<IActionResult> AddMarks(UserProfileModel model)
        {
            if (model.MarksModel != null)
            {
                _znoService.
            }

            return RedirectToAction("Profile", "User");
        }
    }
}
