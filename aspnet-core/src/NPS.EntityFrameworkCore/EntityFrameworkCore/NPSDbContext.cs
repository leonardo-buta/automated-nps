using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using NPS.Authorization.Roles;
using NPS.Authorization.Users;
using NPS.MultiTenancy;
using NPS.Messages;
using NPS.MessageTypes;
using NPS.Campaigns;
using NPS.SendProcesses;
using NPS.Mailings;
using NPS.StatusSendProcesses;
using NPS.SendProcessReports;

namespace NPS.EntityFrameworkCore
{
    public class NPSDbContext : AbpZeroDbContext<Tenant, Role, User, NPSDbContext>
    {
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageType> MessageTypes { get; set; }
        public DbSet<SendProcess> SendProcesses { get; set; }
        public DbSet<StatusSendProcess> StatusSendProcesses { get; set; }
        public DbSet<Mailing> Mailings { get; set; }
        public DbSet<SendProcessReport> SendProcessReport { get; set; }

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

            modelBuilder.Entity<SendProcess>().ToTable("send_processes");
            modelBuilder.Entity<SendProcess>().Property(p => p.Name).IsRequired().HasMaxLength(255);
            modelBuilder.Entity<SendProcess>().Property(p => p.Separator).IsRequired().HasMaxLength(1);
            modelBuilder.Entity<SendProcess>().Property(p => p.UploadedMailing).IsRequired().HasDefaultValue(0);

            modelBuilder.Entity<Mailing>().ToTable("mailings");
            modelBuilder.Entity<Mailing>().Property(p => p.Line).IsRequired().HasMaxLength(500);
            modelBuilder.Entity<Mailing>().Property(p => p.Valid).IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<Mailing>().Property(p => p.Duplicated).IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<Mailing>().Property(p => p.Empty).IsRequired().HasDefaultValue(0);
            modelBuilder.Entity<Mailing>().Property(p => p.IncorretFormat).IsRequired().HasDefaultValue(0);

            modelBuilder.Entity<StatusSendProcess>().ToTable("status_send_process");
            modelBuilder.Entity<StatusSendProcess>().Property(p => p.Name).IsRequired().HasMaxLength(50);

            modelBuilder.Entity<SendProcessReport>().ToTable("send_process_reports");
            modelBuilder.Entity<SendProcessReport>().Property(p => p.Guid).IsRequired();
            modelBuilder.Entity<SendProcessReport>().Property(p => p.Text).IsRequired();
            modelBuilder.Entity<SendProcessReport>().Property(p => p.Recipient).IsRequired();

            // Seed
            modelBuilder.Entity<MessageType>().HasData(new MessageType { Id = 1, Type = "E-Mail" }, new MessageType { Id = 2, Type = "SMS" });
            modelBuilder.Entity<StatusSendProcess>().HasData(new StatusSendProcess { Id = 1, Name = "Pendente" },
                                                             new StatusSendProcess { Id = 2, Name = "Aguardando agendamento" },
                                                             new StatusSendProcess { Id = 3, Name = "Enviando" },
                                                             new StatusSendProcess { Id = 4, Name = "Finalizado" });
            base.OnModelCreating(modelBuilder);
        }
    }
}
