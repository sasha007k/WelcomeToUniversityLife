using Application.IServices;
using Application.Models.SiteAdmin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Domain.Entities;

namespace WelcomeToUniversityLifeAspServer.Controllers
{
    public class SiteAdminController : Controller
    {
        private readonly ISiteAdminService _siteAdminService;
        private readonly IFucker _newsNub;

        private IHubContext<NewsHub> HubContext { get; set; }

        public SiteAdminController(ISiteAdminService siteAdminService, IHubContext<NewsHub> hubcontext)
        {
            _siteAdminService = siteAdminService;
            HubContext = hubcontext;
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

            await this.HubContext.Clients.All.SendAsync("AddUniversity",model.UniversityName);

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
            var message = (string.Empty, new Сampaign());
            if (request != null)
            {
                message = await _siteAdminService.CreateCampaignAsync(request).ConfigureAwait(true); 
            }

            if (message.Item2 != null)
            {
                var enumDisplayStatus = (CampaignStatus)message.Item2.Status;
                await this.HubContext.Clients.All.SendAsync("CreateCampaign", message.Item2.Start, message.Item2.End,
                    enumDisplayStatus.ToString(), message.Item2.Id);
            }
            
            return RedirectToAction("GetAllCampaigns", new { message = message.Empty });
        }

        [HttpGet]
        [Authorize(Roles = "SiteAdmin")]
        public async Task<IActionResult> DeleteCampaign(int campaignId)
        {
            if (campaignId != 0)
            {
                await _siteAdminService.DeleteCampaignAsync(campaignId).ConfigureAwait(true);
                await this.HubContext.Clients.All.SendAsync("DeleteCampaign", campaignId);
            }

            return RedirectToAction("GetAllCampaigns");
        }

        public async Task<IActionResult> News()
        {

            var campaings = await _siteAdminService.GetAllCampaigns().ConfigureAwait(true);
            var universities = _siteAdminService.GetAllUniversities();
            ViewBag.universities = universities;


            return View(campaings);
        }
    }
}