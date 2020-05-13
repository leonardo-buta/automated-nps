using Abp.Domain.Entities.Auditing;
using NPS.Campaings;
using NPS.MessageTypes;

namespace NPS.Messages
{
    public class Message : AuditedEntity
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public MessageType MessageType { get; set; }

        public Campaign Campaing { get; set; }
    }
}
