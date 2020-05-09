using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using NPS.Authorization.Roles;
using NPS.Authorization.Users;
using NPS.MultiTenancy;
using NPS.Campanhas;

namespace NPS.EntityFrameworkCore
{
    public class NPSDbContext : AbpZeroDbContext<Tenant, Role, User, NPSDbContext>
    {
        public DbSet<Mensagens> Mensagens { get; set; }

        public NPSDbContext(DbContextOptions<NPSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
            modelBuilder.Entity<Mensagens>().ToTable("mensagens");
            modelBuilder.Entity<Mensagens>().Property(p => p.Nome).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Mensagens>().Property(p => p.Texto).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
