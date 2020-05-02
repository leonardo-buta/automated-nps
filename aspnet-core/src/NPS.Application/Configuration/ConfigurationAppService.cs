using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using NPS.Configuration.Dto;

namespace NPS.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : NPSAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
