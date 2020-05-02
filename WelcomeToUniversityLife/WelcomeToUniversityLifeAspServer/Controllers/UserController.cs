using Application.IServices;
using Application.Models.User;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IZnoService _znoService;
        IWebHostEnvironment _appEnvironment;

        public UserController(IUserService userService, IZnoService znoService, IWebHostEnvironment appEnvironment)
        {
            _userService = userService;
            _znoService = znoService;
            _appEnvironment = appEnvironment;
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
            if (model?.ChangePasswordModel != null && model.ChangePasswordModel.NewPassword == model.ChangePasswordModel.ConfirmNewPassword)
                await _userService.ChangePassword(model.ChangePasswordModel).ConfigureAwait(true);

            return RedirectToAction("Profile", "User");
        }

        [HttpPost]
        public async Task<IActionResult> AddDocs(IFormFileCollection uploads)
        {
            if (uploads != null)
            {
                var id = await _userService.GetIdByName(User.Identity.Name).ConfigureAwait(true);


                if (!Directory.Exists(Path.Combine(_appEnvironment.WebRootPath, "Docs", $"{id}")))
                {
                    Directory.CreateDirectory(Path.Combine(_appEnvironment.WebRootPath, "Docs", $"{id}"));
                }

                foreach (var item in uploads)
                {
                    var path = Path.Combine("Docs", $"{id}", item.FileName);
                    await using var fileStream = new FileStream(Path.Combine(_appEnvironment.WebRootPath, path), FileMode.Create);
                    var res = await _userService.AddDocs(User.Identity.Name, new Domain.Entities.Document() { Name = item.FileName }).ConfigureAwait(true);
                    if (res.Succeeded)
                    {
                        await item.CopyToAsync(fileStream).ConfigureAwait(true);
                    }
                }

                return RedirectToAction("Profile", "User");
            }

            return BadRequest("Can't save documents");
        }

        public async Task<IActionResult> ApplyButtonExecute(int specialityId, int facultyId)
        {
            var message = string.Empty;
            if (specialityId != 0)
            {
                message = await _userService.ApplyButtonExecuteAsync(specialityId).ConfigureAwait(true);
            }

            return RedirectToAction("GetFaculty", "UniversityAdmin", new { id = facultyId, message = message });
        }

        [HttpPost]
        public async Task<IActionResult> AddMarks(UserProfileModel model)
        {
            if (model?.MarksModel != null)
            {
                model.MarksModel.FirstZnoModel.Name = "Ukrainian";
                await _znoService.SaveZNOMarks(model.MarksModel).ConfigureAwait(true);
            } ;

            return RedirectToAction("Profile", "User");
        }

       
    }
}