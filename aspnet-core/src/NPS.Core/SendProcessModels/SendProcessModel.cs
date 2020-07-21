using Abp.Domain.Entities;

namespace NPS.SendProcessModels
{
    public class SendProcessModel : Entity
    {
        public string Text { get; set; }

        public string Recipient { get; set; }

        public int SendProcessId { get; set; }
    }
}
