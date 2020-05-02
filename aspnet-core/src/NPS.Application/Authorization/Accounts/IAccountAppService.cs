using System.Threading.Tasks;
using Abp.Application.Services;
using NPS.Authorization.Accounts.Dto;

namespace NPS.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
