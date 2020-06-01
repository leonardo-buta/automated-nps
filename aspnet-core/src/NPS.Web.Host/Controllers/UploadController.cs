using Microsoft.AspNetCore.Mvc;
using NPS.Controllers;
using NPS.Mailings;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace NPS.Web.Host.Controllers
{
    public class UploadController : NPSControllerBase
    {
        private readonly IMailingAppService _mailingAppService;

        public UploadController(IMailingAppService mailingAppService)
        {
            _mailingAppService = mailingAppService;
        }

        [HttpPost]
        public async Task<IActionResult> UploadMailing(int sendProcessId, IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                await _mailingAppService.UploadMailing(sendProcessId, formFile);
            }

            return Ok();
        }
    }
}
