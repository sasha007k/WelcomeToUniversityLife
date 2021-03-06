﻿using System;
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
            var universityAndFaculties = await _universityAdminService.GetUniversity();
            if (universityAndFaculties == null)
            {
                return RedirectToAction();
            }

            return View(universityAndFaculties);
        }

        public async Task<ActionResult> GetUniversity(int id)
        {
            var universityAndFaculties = await _universityAdminService.GetUniversityAsync(id);
            if (universityAndFaculties == null)
            {
                return RedirectToAction();
            }

            return View("University", universityAndFaculties);
        }

        public IActionResult EditUniversityInfo(University currentUniversity)
        {
            return View(currentUniversity);
        }

        [HttpPost]
        public async Task<IActionResult> EditUniversityInfo(UniversityInfoModel model)
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
            return View(new AddFacultyModel());
        }

        [HttpPost]
        public async Task<IActionResult> AddFaculty(AddFacultyModel model)
        {
            if (model != null)
            {
                var result = await _universityAdminService.AddFacultyAsync(model);
                if (result)
                {
                    return RedirectToAction("University", "UniversityAdmin");
                }
            }
            return RedirectToAction("AddFaculty", "UniversityAdmin");
        }

        public async Task<ActionResult> GetFaculty(int id)
        {
            var facultyAndSpecialities = await _universityAdminService.GetFacultyAsync(id);
            if (facultyAndSpecialities == null)
            {
                return RedirectToAction();
            }

            return View("Faculty", facultyAndSpecialities);
        }

        public IActionResult AddSpeciality(int facultyId)
        {
            var speciality = new AddSpecialityModel()
            {
                FacultyId = facultyId
            };

            return View(speciality);
        }

        [HttpPost]
        public async Task<IActionResult> AddSpeciality(AddSpecialityModel model)
        {
            //if (model != null)
            //{
            //    var result = await _universityAdminService.AddFacultyAsync(model);
            //    if (result)
            //    {
            //        return RedirectToAction("University", "UniversityAdmin");
            //    }
            //}
            return RedirectToAction("AddFaculty", "UniversityAdmin");
        }
    }
}
