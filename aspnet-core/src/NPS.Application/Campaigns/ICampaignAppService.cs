using Abp.Application.Services;
using NPS.Campaigns.Dto;

namespace NPS.Campaigns
{
    public interface ICampaignAppService : IAsyncCrudAppService<CampaignDto>
    {
        
    }
}
