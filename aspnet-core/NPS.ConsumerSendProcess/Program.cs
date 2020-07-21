using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NPS.ConsumerSendProcess.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Text;
using System.Threading;

namespace NPS.ConsumerSendProcess
{
    // https://github.com/renatogroffe/RabbitMQ_HealthChecks-DotNetCore2.2
    class Program
    {
        private static IConfiguration _configuration;
        private static readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            _configuration = builder.Build();

            var rabbitMQConfiguration = new RabbitMQConfiguration();
            new ConfigureFromConfigurationOptions<RabbitMQConfiguration>(_configuration.GetSection("RabbitMQConfiguration")).Configure(rabbitMQConfiguration);

            var factory = new ConnectionFactory
            {
                HostName = rabbitMQConfiguration.HostName,
                Port = rabbitMQConfiguration.Port,
                UserName = rabbitMQConfiguration.UserName,
                Password = rabbitMQConfiguration.Password
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "TestesASPNETCore",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(queue: "TestesASPNETCore", autoAck: true, consumer: consumer);

                Console.WriteLine("Aguardando mensagens para processamento");

                // Tratando o encerramento da aplicação com
                // Control + C ou Control + Break
                Console.CancelKeyPress += (o, e) =>
                {
                    Console.WriteLine("Saindo...");

                    // Libera a continuação da thread principal
                    _waitHandle.Set();
                    e.Cancel = true;
                };

                // Aguarda que o evento CancelKeyPress ocorra
                _waitHandle.WaitOne();
            }
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.Span;
            var message = JsonConvert.DeserializeObject<SendProcessModel>(Encoding.UTF8.GetString(body));

            // lógica de enviar email

            new Repository(_configuration).InsertSendProcessReport(message);
        }
    }
}
