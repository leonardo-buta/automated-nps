using Abp.Application.Services;
using NPS.SendProcessesReports.Dto;

namespace NPS.SendProcessReports
{
    public interface ISendProcessReportAppService : IAsyncCrudAppService<SendProcessReportDto>
    {
        
    }
}
