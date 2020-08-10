using System;

namespace NPS.ServicesRepository.Models
{
    public class SendProcessModel
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Subject { get; set; }

        public string Text { get; set; }

        public string Recipient { get; set; }
    }
}
