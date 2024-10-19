using MLAB.PlayerEngagement.Core.Models.Reports;

namespace MLAB.PlayerEngagement.Core.Services
{
    public interface IReportsService
    {
        Task<CommunicationReviewReportResponseModel> GetCommunicationReviewReportAsync(CommunicationReviewReportRequestModel request);
        Task<List<CommReviewListResponseModel>> CommunicationReviewReportListingAsync(CommunicationReviewReportRequestModel request);
        Task<List<CommReviewGridResponseModel>> CommunicationReviewReportGridAsync(CommunicationReviewReportRequestModel request);
    }
}
