﻿using Newtonsoft.Json;
using NPS.ServicesCommon;
using NPS.ServicesRepository;
using NPS.ServicesRepository.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace NPS.ConsumerSendProcess
{
    // https://github.com/renatogroffe/RabbitMQ_HealthChecks-DotNetCore2.2
    class Program
    {
        private static readonly AutoResetEvent _waitHandle = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            using (var connection = RabbitMQConnection.GetConnectionFactory().CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "NPS.SendProcess",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, e) =>
                {
                    Consumer_Received(e);
                    channel.BasicAck(e.DeliveryTag, false);
                };

                channel.BasicConsume(queue: "NPS.SendProcess", autoAck: false, consumer: consumer);

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

        private static void Consumer_Received(BasicDeliverEventArgs e)
        {
            var body = e.Body.Span;
            var message = JsonConvert.DeserializeObject<SendProcessModel>(Encoding.UTF8.GetString(body));

            // lógica de enviar email

            new Repository().InsertSendProcessReport(message);
        }
    }
}
