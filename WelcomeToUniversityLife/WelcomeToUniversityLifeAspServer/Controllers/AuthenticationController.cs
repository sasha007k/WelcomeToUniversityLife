using Application.IServices;
using Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid && model != null)
                if (await _authenticationService.FindByEmailAsync(model.Email).ConfigureAwait(true) == null)
                    if ((await _authenticationService.Register(model).ConfigureAwait(true)).Succeeded)
                        return RedirectToAction("Profile", "User");
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
                    return RedirectToAction("AllUniversities", "SiteAdmin");
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
                var (signInResult, role) = await _authenticationService.SignIn(model).ConfigureAwait(true);
                if (signInResult.Succeeded)
                {
                    var route = ChooseStartPage(role);
                    return route;
                }

                ModelState.AddModelError("", "Login or password is incorrect!");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            await _authenticationService.SignOut().ConfigureAwait(true); ;

            return RedirectToAction("SignIn", "Authentication");
        }
    }
}