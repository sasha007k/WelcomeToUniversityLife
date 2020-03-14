using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application.IServices;
using Application.Models.User;

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
            UserProfileModel profile = await _userService.GetUserInfo(this.User.Identity.Name).ConfigureAwait(true);
            return this.View(profile);
        }
    }
}
