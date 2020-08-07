using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NPS.ServicesRepository.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NPS.ServicesRepository
{
    public class Repository
    {
        private static IConfiguration _configuration;

        public Repository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            _configuration = builder.Build();
        }

        public void InsertSendProcessReport(SendProcessModel sendProcessModel)
        {
            using (var con = new MySqlConnection(_configuration.GetConnectionString("Default")))
            {
                var parameters = new DynamicParameters();
                parameters.Add("guid", Guid.NewGuid().ToString());
                parameters.Add("text", sendProcessModel.Text);
                parameters.Add("recipient", sendProcessModel.Recipient);
                parameters.Add("sendProcessId", sendProcessModel.Id);
                parameters.Add("sendDate", DateTime.Now);
                con.ExecuteAsync(@"INSERT INTO send_process_reports (Guid, Text, Recipient, SendProcessId, SendDate)
                                       VALUES (@guid, @text, @recipient, @sendProcessId, @sendDate)", parameters);
            }
        }

        public async Task<IEnumerable<SendProcessModel>> GetSendProcessWaitingSchedule()
        {
            IEnumerable<SendProcessModel> processList = Enumerable.Empty<SendProcessModel>();
            using (var con = new MySqlConnection(_configuration.GetConnectionString("Default")))
            {
                processList = await con.QueryAsync<SendProcessModel>(@"SELECT 
                                                                               p.Id, p.ScheduleDate, m.Subject, m.Text
                                                                           FROM
                                                                               send_processes p
                                                                                   INNER JOIN
                                                                               messages m ON p.MessageId = m.Id
                                                                           WHERE
                                                                               p.StatusSendProcessId = 2;");
            }
            return processList;
        }

        public async Task<IEnumerable<string>> GetMailingsByProcessId(int id)
        {
            IEnumerable<string> mailings = Enumerable.Empty<string>();
            using (var con = new MySqlConnection(_configuration.GetConnectionString("Default")))
            {
                mailings = await con.QueryAsync<string>(@"SELECT 
                                                                    m.Line
                                                                FROM
                                                                    mailings m
                                                                        INNER JOIN
                                                                    send_processes p ON m.ProcessSendId = p.Id
                                                                WHERE
                                                                    p.Id = @id", new { id });
            }

            return mailings;
        }
    }
}
