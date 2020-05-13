using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using NPS.Authorization.Roles;
using NPS.Authorization.Users;
using NPS.MultiTenancy;
using NPS.Messages;
using NPS.MessageTypes;
using NPS.Campaings;

namespace NPS.EntityFrameworkCore
{
    public class NPSDbContext : AbpZeroDbContext<Tenant, Role, User, NPSDbContext>
    {
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Message> Mensagens { get; set; }
        public DbSet<MessageType> MessageTypes { get; set; }

        public NPSDbContext(DbContextOptions<NPSDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().ToTable("messages");
            modelBuilder.Entity<Message>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Message>().Property(p => p.Text).IsRequired();

            modelBuilder.Entity<MessageType>().ToTable("message_types");
            modelBuilder.Entity<MessageType>().Property(p => p.Type).IsRequired().HasMaxLength(20);

            modelBuilder.Entity<Campaign>().ToTable("campaigns");
            modelBuilder.Entity<Campaign>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Campaign>().Property(p => p.Description).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<Campaign>().Property(p => p.StartDate).IsRequired();
            modelBuilder.Entity<Campaign>().Property(p => p.EndDate).IsRequired();
            modelBuilder.Entity<Campaign>().Property(p => p.Active).IsRequired();

            // Seed
            modelBuilder.Entity<MessageType>().HasData(new MessageType { Id = 1, Type = "E-Mail" }, new MessageType { Id = 2, Type = "SMS" });

            base.OnModelCreating(modelBuilder);
        }
    }
}
