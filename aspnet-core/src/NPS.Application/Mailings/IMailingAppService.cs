using Abp.Application.Services;
using Microsoft.AspNetCore.Http;
using NPS.Mailings.Dto;
using System.Threading.Tasks;

namespace NPS.Mailings
{
    public interface IMailingAppService : IAsyncCrudAppService<MailingDto>
    {
        Task UploadMailing(int sendProcessId, IFormFile formFile);
    }
}
