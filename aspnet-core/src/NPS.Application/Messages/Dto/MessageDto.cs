using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;

namespace NPS.Messages.Dto
{
    [AutoMap(typeof(Message))]
    public class MessageDto : EntityDto<int>
    {
        public string Name { get; set; }

        public string Text { get; set; }

        public string MessageType { get; set; }

        public string Campaign { get; set; }

        public DateTime CreationTime { get; set; }
    }
}
