using Abp.Application.Services;
using Abp.Domain.Repositories;
using NPS.SendProcessesReports.Dto;

namespace NPS.SendProcessReports
{
    public class SendProcessReportAppService : AsyncCrudAppService<SendProcessReport, SendProcessReportDto>, ISendProcessReportAppService
    {
        public SendProcessReportAppService(IRepository<SendProcessReport> repository)
            : base(repository)
        {

        }
    }
}
