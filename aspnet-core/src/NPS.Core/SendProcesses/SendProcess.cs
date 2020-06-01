using Abp.Domain.Entities;
using NPS.Messages;
using NPS.StatusSendProcesses;
using System;

namespace NPS.SendProcesses
{
    public class SendProcess : Entity
    {
        public string Name { get; set; }

        public string Separator { get; set; }

        public Message Message { get; set; }

        public DateTime ScheduleDate { get; set; }

        public StatusSendProcess StatusSendProcess { get; set; }

        public bool UploadedMailing { get; set; }
    }
}
