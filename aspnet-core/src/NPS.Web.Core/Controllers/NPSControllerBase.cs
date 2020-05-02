using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace NPS.Controllers
{
    public abstract class NPSControllerBase: AbpController
    {
        protected NPSControllerBase()
        {
            LocalizationSourceName = NPSConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
