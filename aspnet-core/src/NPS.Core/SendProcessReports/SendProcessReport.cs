using Abp.Domain.Entities;
using NPS.SendProcesses;
using System;

namespace NPS.SendProcessReports
{
    public class SendProcessReport : Entity
    {
        public Guid Guid { get; set; }

        public string Text { get; set; }

        public string Recipient { get; set; }

        public SendProcess SendProcess { get; set; }

        public int? Rating { get; set; }

        public DateTime SendDate { get; set; }

        public DateTime? ResponseDate { get; set; }
    }
}
