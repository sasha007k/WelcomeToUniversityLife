using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class SiteAdminController : Controller
    {
        ISiteAdminService _siteAdminService;
        public SiteAdminController(ISiteAdminService siteAdminService)
        {
            _siteAdminService = siteAdminService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
