using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using NPS.Authorization;

namespace NPS
{
    [DependsOn(
        typeof(NPSCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class NPSApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<NPSAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(NPSApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
