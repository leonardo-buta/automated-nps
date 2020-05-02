using System.Threading.Tasks;
using Abp.Application.Services;
using NPS.Sessions.Dto;

namespace NPS.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
