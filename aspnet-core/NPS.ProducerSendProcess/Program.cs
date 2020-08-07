using Newtonsoft.Json;
using NPS.ServicesCommon;
using NPS.ServicesRepository;
using NPS.ServicesRepository.Models;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace NPS.ProducerSendProcess
{
    // https://github.com/renatogroffe/RabbitMQ_HealthChecks-DotNetCore2.2
    class Program
    {
        static async Task Main(string[] args)
        {
            var repository = new Repository();
            var processList = await repository.GetSendProcessWaitingSchedule();

            foreach (var process in processList)
            {
                var mailings = await repository.GetMailingsByProcessId(process.Id);

                using (var connection = RabbitMQConnection.GetConnectionFactory().CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "NPS.SendProcess",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    foreach (var mailing in mailings)
                    {
                        var message = JsonConvert.SerializeObject(new SendProcessModel
                        {
                            Id = process.Id,
                            Recipient = mailing,
                            Subject = process.Subject,
                            Text = process.Text
                        });

                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "NPS.SendProcess",
                                             basicProperties: null,
                                             body: body);
                    }
                }
            }
        }
    }
}
