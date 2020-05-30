using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace NPS.Campaigns.Dto
{
    [AutoMap(typeof(Campaign))]
    public class CampaignDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Active { get; set; }
    }
}
