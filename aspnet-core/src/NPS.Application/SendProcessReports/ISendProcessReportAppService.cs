using Abp.Application.Services;
using NPS.SendProcessesReports.Dto;
using System;
using System.Threading.Tasks;

namespace NPS.SendProcessReports
{
    public interface ISendProcessReportAppService : IAsyncCrudAppService<SendProcessReportDto>
    {
        Task AnswerNPS(Guid guid, int rating);
    }
}
