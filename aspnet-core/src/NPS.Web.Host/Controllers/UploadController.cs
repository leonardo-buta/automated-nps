using Microsoft.AspNetCore.Mvc;
using NPS.Controllers;
using NPS.Mailings;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

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
        public async Task<JsonResult> Mailing(int sendProcessId, string separator, IFormFile formFile)
        {
            try
            {
                if (sendProcessId > 0 && formFile.Length > 0)
                {
                    await _mailingAppService.UploadMailing(sendProcessId, separator, formFile);
                }

                return Json(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
