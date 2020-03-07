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
        public IActionResult Register([FromBody]RegisterModel request)
        {
            return View();
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn([FromBody]SignInModel request)
        {
            return View();
        }
    }
}