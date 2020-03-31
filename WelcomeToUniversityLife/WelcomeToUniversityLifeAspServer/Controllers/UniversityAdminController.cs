using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.UniversityAdmin;
using Domain.Entities;
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
        public async Task<ActionResult> University()
        {
            var university = await _universityAdminService.GetUniversity();
            if (university == null)
            {
                return RedirectToAction();
            }

            return View(university);
        }

        public async Task<ActionResult> GetUniversity(int id)
        {
            var university = await _universityAdminService.GetUniversityAsync(id);
            if (university == null)
            {
                return RedirectToAction();
            }

            return View("University", university);
        }

        public IActionResult EditUniversityInfo(University currentUniversity)
        {
            return View(currentUniversity);
        }

        [HttpPost]
        public async Task<IActionResult> EditUniversityInfo(UniversityInfo model)
        {
            var result = false;
            if (model != null)
            {
                result = await _universityAdminService.EditUniversity(model).ConfigureAwait(true);
            }


            if (!result)
            {
                // do smth
            }

            return RedirectToAction("University", "UniversityAdmin");
        }

        public IActionResult AddFaculty()
        {
            return View();
        }
    }
}
