using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using NPS.SendProcessReports;
using System;

namespace NPS.SendProcessesReports.Dto
{
    [AutoMap(typeof(SendProcessReport))]
    public class SendProcessReportDto : EntityDto<int>
    {
        public string Recipient { get; set; }

        public int? Rating { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}
