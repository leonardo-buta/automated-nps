using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace NPS.SendProcesses.Dto
{
    [AutoMap(typeof(SendProcess))]
    public class GetAllSendProcessInput : PagedAndSortedResultRequestDto
    {
        public string Name { get; set; }

        //public int StatusSendProcessId { get; set; }
    }
}
