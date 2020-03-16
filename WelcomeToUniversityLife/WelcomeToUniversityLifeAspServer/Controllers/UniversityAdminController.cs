using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class UniversityAdminController : Controller
    {
        IUniversityAdminService _universityAdminService;
        public UniversityAdminController(IUniversityAdminService universityAdminService)
        {
            _universityAdminService = universityAdminService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
