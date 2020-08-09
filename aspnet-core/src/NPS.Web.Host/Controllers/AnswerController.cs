using Microsoft.AspNetCore.Mvc;
using NPS.Controllers;
using System.Threading.Tasks;
using System;
using NPS.SendProcessReports;
using System.Text;

namespace NPS.Web.Host.Controllers
{
    public class AnswerController : NPSControllerBase
    {
        private readonly ISendProcessReportAppService _sendProcessReportAppService;

        public AnswerController(ISendProcessReportAppService sendProcessReportAppService)
        {
            _sendProcessReportAppService = sendProcessReportAppService;
        }

        [HttpPost]
        public async Task<JsonResult> SendResponse(Guid guid, string rating)
        {
            try
            {
                int internalRating = Convert.ToInt32(Encoding.UTF8.GetString(Convert.FromBase64String(rating)));

                if (internalRating < 0) internalRating = 0;
                if (internalRating > 10) internalRating = 10;

                await _sendProcessReportAppService.AnswerNPS(guid, internalRating);

                return Json(string.Empty);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}
