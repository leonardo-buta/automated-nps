using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace NPS.SendProcesses.Dto
{
    [AutoMap(typeof(SendProcess))]
    public class SendProcessDto : EntityDto<int>
    {
        public string Name { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string StatusSendProcess { get; set; }
    }
}
