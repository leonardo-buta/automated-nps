using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace NPS.EntityFrameworkCore
{
    public static class NPSDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<NPSDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<NPSDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
