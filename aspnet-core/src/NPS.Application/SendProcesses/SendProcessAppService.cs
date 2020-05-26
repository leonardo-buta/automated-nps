using Abp.Application.Services;
using Abp.Domain.Repositories;
using NPS.SendProcesses;
using NPS.SendProcesses.Dto;

namespace NPS.Campaigns
{
    public class SendProcessAppService : AsyncCrudAppService<SendProcess, SendProcessDto>, ISendProcessAppService
    {
        public SendProcessAppService(IRepository<SendProcess> repository)
            : base(repository)
        {
            
        }
    }
}
