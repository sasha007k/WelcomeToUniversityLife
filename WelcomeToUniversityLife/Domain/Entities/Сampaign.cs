using System;

namespace Domain.Entities
{
    public enum CampaignStatus
    {
        Active,
        Closed,
        Pending
    }

    public class Сampaign
    {
        public int Id { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public CampaignStatus Status { get; set; }
    }
}