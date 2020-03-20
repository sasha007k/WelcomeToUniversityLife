﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.SiteAdmin;
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
        public IActionResult Universities()
        {
            //var universities = await _siteAdminService
            return View();
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

            return RedirectToAction("Universities", "SiteAdmin");
        }
    }
}
