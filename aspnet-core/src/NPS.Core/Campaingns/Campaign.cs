using Abp.Domain.Entities.Auditing;
using System;

namespace NPS.Campaings
{
    public class Campaign : AuditedEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Active { get; set; }
    }
}
