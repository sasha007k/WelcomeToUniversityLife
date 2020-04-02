using System;
using Application.IServices;
using Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class AuthenticationController : Controller
    {
        IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public  IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _authenticationService.FindByEmailAsync(model.Email) == null)
                {
                    if ((await _authenticationService.Register(model)).Succeeded)
                    {
                        return RedirectToAction("Profile", "User");
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        private IActionResult ChooseStartPage(string role)
        {
            switch (role)
            {
                case "User":
                    return RedirectToAction("Profile", "User");
                case "UniversityAdmin":
                    return RedirectToAction("University", "UniversityAdmin");
                case "SiteAdmin":
                    return RedirectToAction("AllUniversities", "SiteAdmin");
            }
            return RedirectToAction();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            if (ModelState.IsValid)
            {
                var (signInResult, role) = await _authenticationService.SignIn(model);
                if (signInResult.Succeeded)
                {
                    var route = ChooseStartPage(role);
                    return route;
                }
                else
                {
                    ModelState.AddModelError("", "Login or password is incorrect!");
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _authenticationService.SignOut();

            return RedirectToAction("SignIn", "Authentication");
        }
    }
}