using Foolproof;
using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.SiteAdmin
{
    public class CampaignModel
    {
        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}
