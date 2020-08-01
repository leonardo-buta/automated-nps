using Abp.Domain.Entities.Auditing;
using NPS.Campaigns;
using NPS.MessageTypes;

namespace NPS.Messages
{
    public class Message : AuditedEntity
    {
        public string Name { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public MessageType MessageType { get; set; }

        public Campaign Campaign { get; set; }
    }
}
