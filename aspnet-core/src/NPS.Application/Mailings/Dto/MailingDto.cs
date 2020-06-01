using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace NPS.Mailings.Dto
{
    [AutoMap(typeof(Mailing))]
    public class MailingDto : EntityDto<int>
    {
        public int ProcessSendId { get; set; }

        public string Line { get; set; }

        public bool Valid { get; set; }

        public bool Duplicated { get; set; }

        public bool Empty { get; set; }

        public bool IncorretFormat { get; set; }
    }
}
