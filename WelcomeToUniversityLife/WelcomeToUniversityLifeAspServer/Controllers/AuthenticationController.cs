using Application.Models.Authentication;
using Microsoft.AspNetCore.Mvc;


namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class AuthenticationController : Controller
    {
        public AuthenticationController()
        {

        }

        [HttpGet]
        public  IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterModel request)
        {
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(SignInModel request)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}