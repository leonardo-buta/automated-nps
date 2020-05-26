using Abp.Domain.Entities;
using NPS.SendProcesses;

namespace NPS.Mailings
{
    public class Mailing : Entity
    {
        public SendProcess ProcessSend { get; set; }

        public string Line { get; set; }

        public bool Valid { get; set; }

        public bool Duplicated { get; set; }

        public bool Empty { get; set; }

        public bool IncorretFormat { get; set; }
    }
}
