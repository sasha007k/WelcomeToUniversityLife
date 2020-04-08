using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.IServices;
using Application.Models.UniversityAdmin;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

            ViewBag.iseditable = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

                ViewBag.iseditable = userId == universityAndFaculties.CurrentUniversity.UserId;
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

            ViewBag.iseditable = false;

            if (User.Identity.IsAuthenticated)
            {
               var userId=Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

                ViewBag.iseditable = userId == universityAndFaculties.CurrentUniversity.UserId;        
            }

            return View("University", universityAndFaculties);
        }

        [HttpPost]
        public async Task<IActionResult> EditUniversityInfo(University model)
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

            ViewBag.iseditable = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

                ViewBag.iseditable = userId == facultyAndSpecialities.FacultyAdminId;
            }

            return View("Faculty", facultyAndSpecialities);
        }

        [HttpPost]
        public async Task<IActionResult> AddSpeciality(AddSpecialityModel model)
        {
            if (model != null)
            {
                var result = await _universityAdminService.AddSpecialityAsync(model);
                if (result)
                {
                    return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = model.FacultyId });
                }
            }
            return RedirectToAction("AddSpeciality", "UniversityAdmin");
        }

        [HttpPost]
        [Authorize(Roles = "UniversityAdmin")]
        public async Task<IActionResult> UploadUniversityPhoto([FromQuery]UploadPhotoModel requestData, IFormFileCollection uploadedFiles)
        {
            requestData.requestedUserId= Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            await _universityAdminService.UploadUniversityPhotoAsync(requestData, uploadedFiles);

            return RedirectToAction("University","UniversityAdmin");
        }

        [HttpGet]
        [Authorize(Roles = "UniversityAdmin")]
        public async Task<IActionResult> DeleteUniversityPhoto()
        {
            var userId = Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            await _universityAdminService.DeleteUniversityPhotoAsync(userId);

            return RedirectToAction("University", "UniversityAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> EditFacultyInfo(Faculty model)
        {
            var result = false;
            if (model != null)
            {
                result = await _universityAdminService.EditFaculty(model).ConfigureAwait(true);
            }

            if (!result)
            {
                // do smth
            }

            return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = model.Id });
        }
    }
}
