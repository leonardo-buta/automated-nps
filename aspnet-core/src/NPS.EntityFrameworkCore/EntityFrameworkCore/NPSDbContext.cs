using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using NPS.Authorization.Roles;
using NPS.Authorization.Users;
using NPS.MultiTenancy;

namespace NPS.EntityFrameworkCore
{
    public class NPSDbContext : AbpZeroDbContext<Tenant, Role, User, NPSDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public NPSDbContext(DbContextOptions<NPSDbContext> options)
            : base(options)
        {
        }
    }
}
