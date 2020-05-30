using Abp.Domain.Entities;
using NPS.Campaigns;
using NPS.Messages;
using System;

namespace NPS.SendProcesses
{
    public class SendProcess : Entity
    {
        public string Name { get; set; }

        public string Separator { get; set; }

        public Campaign Campaign { get; set; }

        public Message Message { get; set; }

        public DateTime SendDate { get; set; }

        public bool UploadedMailing { get; set; }
    }
}
