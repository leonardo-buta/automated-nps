using Abp.Application.Services;
using NPS.MultiTenancy.Dto;

namespace NPS.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

