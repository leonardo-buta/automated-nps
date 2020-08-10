using Abp.Application.Services;
using Abp.Domain.Repositories;
using NPS.SendProcessesReports.Dto;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NPS.SendProcessReports
{
    public class SendProcessReportAppService : AsyncCrudAppService<SendProcessReport, SendProcessReportDto>, ISendProcessReportAppService
    {
        public SendProcessReportAppService(IRepository<SendProcessReport> repository)
            : base(repository)
        {

        }

        public async Task AnswerNPS(Guid guid, int rating)
        {
            var report = Repository.GetAll().Where(x => x.Guid == guid && !x.Rating.HasValue).FirstOrDefault();

            if (report != null)
            {
                report.Rating = rating;
                report.ResponseDate = DateTime.Now;
                await Repository.UpdateAsync(report);
            }
        }
    }
}