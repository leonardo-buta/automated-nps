using System.Threading.Tasks;
using NPS.Configuration.Dto;

namespace NPS.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
