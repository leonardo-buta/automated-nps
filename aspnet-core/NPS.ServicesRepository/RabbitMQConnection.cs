using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.IO;

namespace NPS.ServicesCommon
{
    public static class RabbitMQConnection
    {
        private static RabbitMQConfiguration _rabbitMQConfiguration;

        static RabbitMQConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            IConfiguration configuration = builder.Build();

            _rabbitMQConfiguration = new RabbitMQConfiguration();
            new ConfigureFromConfigurationOptions<RabbitMQConfiguration>(configuration.GetSection("RabbitMQConfiguration")).Configure(_rabbitMQConfiguration);
        }

        public static ConnectionFactory GetConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = _rabbitMQConfiguration.HostName,
                Port = _rabbitMQConfiguration.Port,
                UserName = _rabbitMQConfiguration.UserName,
                Password = _rabbitMQConfiguration.Password
            };
        }
    }
}
