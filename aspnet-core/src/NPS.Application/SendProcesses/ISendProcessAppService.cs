using Abp.Application.Services;
using NPS.SendProcesses.Dto;

namespace NPS.SendProcesses
{
    public interface ISendProcessAppService : IAsyncCrudAppService<SendProcessDto, int, GetAllSendProcessInput, CreateSendProcessInput, UpdateSendProcessInput>
    {
        
    }
}
