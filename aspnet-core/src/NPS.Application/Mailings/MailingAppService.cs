using Abp.Application.Services;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NPS.Mailings.Dto;
using System.IO;
using System.Threading.Tasks;

namespace NPS.Mailings
{
    public class MailingAppService : AsyncCrudAppService<Mailing, MailingDto>, IMailingAppService
    {
        private readonly IWebHostEnvironment _env;

        public MailingAppService(IRepository<Mailing> repository, IWebHostEnvironment env)
            : base(repository)
        {
            _env = env;
        }

        public async Task UploadMailing(int sendProcessId, IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                var fileFolder = Path.Combine(_env.WebRootPath, "Mailings", sendProcessId.ToString());
                var filePath = Path.Combine(fileFolder, formFile.FileName);

                Directory.CreateDirectory(fileFolder);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(fileStream);                
            }
        }
    }
}
