﻿using System.Threading.Tasks;
using Application.IServices;
using Application.Models.SiteAdmin;
using Microsoft.AspNetCore.Mvc;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class SiteAdminController : Controller
    {
        private readonly ISiteAdminService _siteAdminService;

        public SiteAdminController(ISiteAdminService siteAdminService)
        {
            _siteAdminService = siteAdminService;
        }

        public ActionResult AllUniversities()
        {
            var universities = _siteAdminService.GetAllUniversities();
            return View(universities);
        }

        public IActionResult AddUniversity()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddUniversity(AddUniversityModel model)
        {
            var result = await _siteAdminService.AddUniversityAsync(model).ConfigureAwait(true);

            if (!result)
            {
                // do smth
            }

            return RedirectToAction("AllUniversities", "SiteAdmin");
        }
    }
}