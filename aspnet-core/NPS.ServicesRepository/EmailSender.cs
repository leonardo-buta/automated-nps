using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NPS.ServicesCommon.Models;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace NPS.ServicesRepository
{
    //http://www.macoratti.net/18/04/aspcoremvc_email1.htm
    public class EmailSender
    {
        private static IConfiguration _configuration;

        public EmailSender()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json");

            _configuration = builder.Build();
        }

        public void SendEmail(string email, string subject, string message)
        {
            try
            {
                EmailSettings emailSettings = new EmailSettings();
                new ConfigureFromConfigurationOptions<EmailSettings>(_configuration.GetSection("EmailSettings")).Configure(emailSettings);

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(emailSettings.UsernameEmail, "Automated NPS")
                };

                mail.To.Add(new MailAddress(email));
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient(emailSettings.PrimaryDomain, emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(emailSettings.UsernameEmail, emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
