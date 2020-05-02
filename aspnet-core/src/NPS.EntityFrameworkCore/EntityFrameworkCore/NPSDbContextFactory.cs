using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NPS.Configuration;
using NPS.Web;

namespace NPS.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class NPSDbContextFactory : IDesignTimeDbContextFactory<NPSDbContext>
    {
        public NPSDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NPSDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            NPSDbContextConfigurer.Configure(builder, configuration.GetConnectionString(NPSConsts.ConnectionStringName));

            return new NPSDbContext(builder.Options);
        }
    }
}
