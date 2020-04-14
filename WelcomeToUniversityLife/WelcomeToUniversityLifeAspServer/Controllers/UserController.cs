using System.Threading.Tasks;
using Application.IServices;
using Application.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IZnoService _znoService;

        public UserController(IUserService userService, IZnoService znoService)
        {
            _userService = userService;
            _znoService = znoService;
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var profile = await _userService.GetUserInfo(User.Identity.Name).ConfigureAwait(true);
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> PersonalInfo(UserProfileModel model)
        {
            await _userService.UpdateUserInfo(model).ConfigureAwait(true);

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserProfileModel model)
        {
            if (model.ChangePasswordModel != null &&
                model.ChangePasswordModel.NewPassword == model.ChangePasswordModel.ConfirmNewPassword)
                await _userService.ChangePassword(model.ChangePasswordModel).ConfigureAwait(true);

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> AddDocs(IFormFileCollection uploads)
        {
            if (uploads != null)
            {
                var result = await _userService.AddDocs(User.Identity.Name, uploads);
                if (result.Succeeded) return RedirectToAction("Profile", "User");
            }

            return BadRequest("Can't save documents");
        }

        public async Task<IActionResult> ApplyButtonExecute(int specialityId, int facultyId)
        {
            var message = string.Empty;
            if (specialityId != 0)
            {
                message = await _userService.ApplyButtonExecuteAsync(specialityId);
            }

            return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = facultyId, message = message});
        }

        [HttpPost]
        public async Task<IActionResult> AddMarks(UserProfileModel model)
        {
            model.MarksModel.FirstZnoModel.Name = "Ukrainian";
            if (model.MarksModel != null) await _znoService.SaveZNOMarks(model.MarksModel);

            return RedirectToAction("Profile", "User");
        }
    }
}