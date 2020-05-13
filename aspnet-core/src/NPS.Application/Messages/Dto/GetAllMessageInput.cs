using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace NPS.Messages.Dto
{
    [AutoMap(typeof(Message))]
    public class GetAllMessageInput : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }

        public string Text { get; set; }
    }
}
