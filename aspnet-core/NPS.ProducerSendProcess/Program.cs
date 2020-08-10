using Newtonsoft.Json;
using NPS.ServicesCommon;
using NPS.ServicesRepository;
using NPS.ServicesRepository.Models;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace NPS.ProducerSendProcess
{
    // https://github.com/renatogroffe/RabbitMQ_HealthChecks-DotNetCore2.2
    class Program
    {
        static Repository _repository = new Repository();

        static async Task Main(string[] args)
        {
            var processList = await _repository.GetSendProcessWaitingSchedule();

            foreach (var process in processList)
            {
                var mailings = await _repository.GetMailingsByProcessId(process.Id);
                var email = await _repository.GetEmailTemplateById(1);

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
                        Guid guid = Guid.NewGuid();
                        string text = FormatText(email, process.Text, guid);

                        var message = JsonConvert.SerializeObject(new SendProcessModel
                        {
                            Id = process.Id,
                            Guid = guid,
                            Recipient = mailing,
                            Subject = process.Subject,
                            Text = text
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

        private static string FormatText(string email, string messageText, Guid guid)
        {
            email = email.Replace("@TEXT@", messageText);
            email = email.Replace("@URL@", _repository.GetAPIUrl() + guid);

            return email;
        }
    }
}
