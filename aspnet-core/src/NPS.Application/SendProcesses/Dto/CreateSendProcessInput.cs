using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace NPS.SendProcesses.Dto
{
    [AutoMap(typeof(SendProcess))]
    public class CreateSendProcessInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Separator { get; set; }

        [Required]
        public int MessageId { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }
    }
}
