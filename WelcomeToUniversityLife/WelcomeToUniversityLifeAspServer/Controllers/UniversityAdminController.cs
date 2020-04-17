using Application.IServices;
using Application.IServices.UniversityAdmin;
using Application.Models.SpecialityModels;
using Application.Models.UniversityAdmin;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class UniversityAdminController : Controller
    {
        private readonly IFacultyService _facultyService;
        private readonly ILogger<UniversityAdminController> _log;
        private readonly ISpecialityService _specialityService;
        private readonly IUniversityService _universityService;

        public UniversityAdminController(IUniversityService universityService, IFacultyService facultyService,
            ISpecialityService specialityService, ILogger<UniversityAdminController> log)
        {
            _universityService = universityService;
            _facultyService = facultyService;
            _specialityService = specialityService;
            _log = log;
        }

        #region Speciality

        [HttpGet]
        public IActionResult Rating()
        {
            return View();
        }

        [HttpGet]
        public async Task<SpecialityRating> GetSpecialityRatingInfo([FromQuery] int id)
        {
            var responce = await _specialityService.GetSpecialityRatingAsync(id).ConfigureAwait(true); ;

            return responce;
        }

        [HttpGet]
        public async Task<List<SpecialityInfoModel>> SearchSpeciality([FromQuery] string filter)
        {
            var responce = await _specialityService.SearchSpecialityAsync(filter).ConfigureAwait(true); ;

            return responce;
        }

        [HttpPost]
        public async Task<IActionResult> AddSpeciality(AddSpecialityModel model)
        {
            if (model != null)
            {
                var result = await _specialityService.AddSpecialityAsync(model).ConfigureAwait(true); ;
                if (result) return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = model.FacultyId });
            }

            return RedirectToAction("AddSpeciality", "UniversityAdmin");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSpeciality(IdModel model)
        {
            if (model != null && model.Id != 0 && model.FacultyId != 0)
            {
                var result = await _specialityService.DeleteSpecialityAndSaveAsync(model.Id).ConfigureAwait(true);
                if (result)
                {
                    _log.LogInformation("Speciality was deleted");
                    return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = model.FacultyId });
                }
            }
            return RedirectToAction("University", "UniversityAdmin");
        }

        #endregion

        #region University

        public async Task<ActionResult> University()
        {
            var universityAndFaculties = await _universityService.GetUniversity().ConfigureAwait(true);
            if (universityAndFaculties == null) return RedirectToAction();

            ViewBag.iseditable = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId =
                    Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

                ViewBag.iseditable = userId == universityAndFaculties.CurrentUniversity.UserId;
            }


            _log.LogInformation("Show university");
            return View(universityAndFaculties);
        }

        public async Task<ActionResult> GetUniversity(int id)
        {
            var universityAndFaculties = await _universityService.GetUniversityAsync(id).ConfigureAwait(true);
            if (universityAndFaculties == null) return RedirectToAction();

            ViewBag.iseditable = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId =
                    Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

                ViewBag.iseditable = userId == universityAndFaculties.CurrentUniversity.UserId;
            }

            _log.LogInformation("Show university");
            return View("University", universityAndFaculties);
        }

        [HttpPost]
        public async Task<IActionResult> EditUniversityInfo(University model)
        {
            var result = false;
            if (model != null) result = await _universityService.EditUniversity(model).ConfigureAwait(true);

            if (!result)
            {
                // do smth
            }

            _log.LogInformation("Show edit university info");
            return RedirectToAction("University", "UniversityAdmin");
        }

        [HttpPost]
        [Authorize(Roles = "UniversityAdmin")]
        public async Task<IActionResult> UploadUniversityPhoto([FromQuery] UploadPhotoModel requestData,
            IFormFileCollection uploadedFiles)
        {
            if(requestData != null)
            {
                requestData.requestedUserId =
                    Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                await _universityService.UploadUniversityPhotoAsync(requestData, uploadedFiles).ConfigureAwait(true);

            }

            return RedirectToAction("University", "UniversityAdmin");
        }

        [HttpGet]
        [Authorize(Roles = "UniversityAdmin")]
        public async Task<IActionResult> DeleteUniversityPhoto()
        {
            var userId =
                Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

            await _universityService.DeleteUniversityPhotoAsync(userId).ConfigureAwait(true);

            return RedirectToAction("University", "UniversityAdmin");
        }

        #endregion

        #region Faculty

        [HttpPost]
        public async Task<IActionResult> AddFaculty(AddFacultyModel model)
        {
            if (model != null)
            {
                var result = await _facultyService.AddFacultyAsync(model).ConfigureAwait(true); ;
                if (result) return RedirectToAction("University", "UniversityAdmin");
            }

            _log.LogInformation("Show add faculty info");
            return RedirectToAction("AddFaculty", "UniversityAdmin");
        }

        public async Task<ActionResult> GetFaculty(int id, string message = null)
        {
            var facultyAndSpecialities = await _facultyService.GetFacultyAsync(id).ConfigureAwait(true); ;
            if (facultyAndSpecialities == null) return RedirectToAction();

            ViewBag.iseditable = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId =
                    Convert.ToInt32(User.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier).Value);

                ViewBag.iseditable = userId == facultyAndSpecialities.FacultyAdminId;
            }

            ViewBag.Message = message;

            return View("Faculty", facultyAndSpecialities);
        }

        [HttpPost]
        public async Task<IActionResult> EditFacultyInfo(Faculty model)
        {
            if (model != null)
            {
                await _facultyService.EditFaculty(model).ConfigureAwait(true);
                return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = model.Id });
            }

            return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteFaculty(int facultyId)
        {
            if (facultyId != 0)
            {
                var result = await _facultyService.DeleteFaculty(facultyId).ConfigureAwait(true); ;
                if (result)
                {
                    _log.LogInformation("Faculty was deleted");
                    return RedirectToAction("University", "UniversityAdmin");
                }
            }
            return RedirectToAction("University", "UniversityAdmin");
        }

        #endregion
    }
}