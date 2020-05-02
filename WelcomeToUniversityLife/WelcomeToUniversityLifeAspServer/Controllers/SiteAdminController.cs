using Application.IServices;
using Application.Models.SiteAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<IActionResult> GetAllCampaigns(string message = null)
        {
            var campaings = await _siteAdminService.GetAllCampaigns().ConfigureAwait(true); 

            if (!string.IsNullOrWhiteSpace(message))
            {
                ViewBag.Message = message;
            }

            return View(campaings);
        }

        [HttpPost]
        [Authorize(Roles = "SiteAdmin")]
        public async Task<IActionResult> CreateCampaign(CampaignModel request)
        {
            var message = string.Empty;
            if (request != null)
            {
                message = await _siteAdminService.CreateCampaignAsync(request).ConfigureAwait(true); ;
            }

            return RedirectToAction("GetAllCampaigns", new { message = message });
        }

        [HttpGet]
        [Authorize(Roles = "SiteAdmin")]
        public async Task<IActionResult> DeleteCampaign(int campaignId)
        {
            if (campaignId != 0)
            {
                await _siteAdminService.DeleteCampaignAsync(campaignId).ConfigureAwait(true); ;
            }

            return RedirectToAction("GetAllCampaigns");
        }

        public async Task<IActionResult> News()
        {

            var campaings = await _siteAdminService.GetAllCampaigns().ConfigureAwait(true);

           

            return View(campaings);
        }
    }
}