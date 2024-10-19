using MLAB.PlayerEngagement.Core.Models.Reports;

namespace MLAB.PlayerEngagement.Core.Repositories
{
    public interface IReportsFactory
    {
        Task<Tuple<List<CommunicationReviewScoreData>, List<CommunicationReviewRemarks>>> GetCommunicationReviewReportAsync(CommunicationReviewReportRequestModel request);
        Task<List<CommReviewListResponseModel>> CommunicationReviewReportListingAsync(CommunicationReviewReportRequestModel request);
        Task<List<CommReviewGridResponseModel>> CommunicationReviewReportGridAsync(CommunicationReviewReportRequestModel request);

    }
}
