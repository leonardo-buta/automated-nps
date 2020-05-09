using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace NPS.Messages.Dto
{
    [AutoMap(typeof(Message))]
    public class MessageDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public int MessageTypeId { get; set; }

        public string MessageType { get; set; }
    }
}
