using Abp.Application.Services;
using Abp.Domain.Repositories;
using NPS.Campaigns.Dto;
using NPS.Campaings;

namespace NPS.Campaigns
{
    public class CampaignAppService : AsyncCrudAppService<Campaign, CampaignDto>, ICampaignAppService
    {
        public CampaignAppService(IRepository<Campaign> repository)
            : base(repository)
        {
            
        }
    }
}
