using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NPS.ConsumerSendProcess.Models;
using System;

namespace NPS.ConsumerSendProcess
{
    public class Repository
    {
        private static IConfiguration _configuration;

        public Repository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void InsertSendProcessReport(SendProcessModel sendProcessModel)
        {
            using (var conexao = new MySqlConnection(_configuration.GetConnectionString("Default")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("guid", Guid.NewGuid().ToString());
                parameters.Add("text", sendProcessModel.Text);
                parameters.Add("recipient", sendProcessModel.Recipient);
                parameters.Add("sendProcessId", sendProcessModel.SendProcessId);
                parameters.Add("sendDate", DateTime.Now);
                conexao.ExecuteAsync(@"INSERT INTO send_process_reports (Guid, Text, Recipient, SendProcessId, SendDate)
                                       VALUES (@guid, @text, @recipient, @sendProcessId, @sendDate)", parameters);
            }
        }
    }
}
