using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace NPS.SendProcesses.Dto
{
    [AutoMap(typeof(SendProcess))]
    public class SendProcessDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Separator { get; set; }

        public int CampaignId { get; set; }

        public int MessageId { get; set; }

        public DateTime SendDate { get; set; }
    }
}
