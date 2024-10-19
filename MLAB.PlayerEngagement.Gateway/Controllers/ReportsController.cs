using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Core.Models.Reports;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportsController : BaseController
    {
        private readonly IReportsService _reportsService;

        public ReportsController(IReportsService reportsService)
        {
            _reportsService = reportsService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportCommunicationReviewReport(CommunicationReviewReportRequestModel request)
        {
            var result = await _reportsService.GetCommunicationReviewReportAsync(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CommunicationReviewReportListingAsync(CommunicationReviewReportRequestModel request)
        {
            var result = await _reportsService.CommunicationReviewReportListingAsync(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CommunicationReviewReportGridAsync(CommunicationReviewReportRequestModel request)
        {
            var result = await _reportsService.CommunicationReviewReportGridAsync(request);
            return Ok(result);
        }


    }
}
