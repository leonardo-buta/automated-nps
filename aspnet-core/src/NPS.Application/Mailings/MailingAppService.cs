using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NPS.Mailings.Dto;
using NPS.SendProcesses;
using NPS.StatusSendProcesses;
using System.IO;
using System.Threading.Tasks;

namespace NPS.Mailings
{
    public class MailingAppService : AsyncCrudAppService<Mailing, MailingDto>, IMailingAppService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IRepository<SendProcess> _sendProcessrepository;

        public MailingAppService(IRepository<Mailing> repository, IRepository<SendProcess> sendProcessrepository, IWebHostEnvironment env)
            : base(repository)
        {
            _env = env;
            _sendProcessrepository = sendProcessrepository;
        }

        public async Task UploadMailing(int sendProcessId, string separator, IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                var fileFolder = Path.Combine(_env.WebRootPath, "Mailings", sendProcessId.ToString());
                var filePath = Path.Combine(fileFolder, formFile.FileName);

                Directory.CreateDirectory(fileFolder);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(fileStream);
                }

                await ProcessFile(sendProcessId, filePath, separator);
            }
        }

        private async Task ProcessFile(int sendProcessId, string filePath, string separator)
        {
            await Repository.DeleteAsync(x => x.ProcessSend.Id == sendProcessId);

            string line;
            StreamReader file = new StreamReader(filePath);
            var sendProcess = await _sendProcessrepository.GetAsync(sendProcessId);
            while ((line = file.ReadLine()) != null)
            {
                string[] mailing = line.Split(separator);

                foreach (string item in mailing)
                {
                    await Repository.InsertAsync(new Mailing
                    {
                        Line = item,
                        Duplicated = false,
                        Empty = false,
                        IncorretFormat = false,
                        ProcessSend = sendProcess
                    });
                }
            }
        }
    }
}
