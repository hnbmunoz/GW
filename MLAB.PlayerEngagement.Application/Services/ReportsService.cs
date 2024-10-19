using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Models.Reports;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace MLAB.PlayerEngagement.Application.Services
{
    public class ReportsService : IReportsService
    {
        private readonly ILogger<ReportsService> _logger;
        private readonly IReportsFactory _reportsFactory;

        public ReportsService( ILogger<ReportsService> logger, IReportsFactory reportsFactory)
        {
            _logger = logger;
            _reportsFactory = reportsFactory;
        }

        public async Task<CommunicationReviewReportResponseModel> GetCommunicationReviewReportAsync(CommunicationReviewReportRequestModel request)
        {
            try
            {
                _logger.LogInfo($"ReportsService | GetCommunicationReviewReportAsync - {JsonConvert.SerializeObject(request)}");

                List<CommunicationReviewScoreData> scoreData = new List<CommunicationReviewScoreData>();
                List<CommunicationReviewRemarks> remarksData = new List<CommunicationReviewRemarks>();

                var result = await _reportsFactory.GetCommunicationReviewReportAsync(request);

                foreach (CommunicationReviewScoreData score in result.Item1 )
                {
                    var isExist = scoreData.Exists(x => x.ReviewID == score.ReviewID);
                    if(!isExist)
                    {
                        var scoreItem = new CommunicationReviewScoreData
                        {
                            PeriodName = score.PeriodName,
                            Reviewer = score.Reviewer,
                            Reviewee = score.Reviewee,
                            RevieweeTeamName = string.Join(",", result.Item1.Where(i => i.ReviewID == score.ReviewID).Select(j => j.RevieweeTeamName.ToString())),
                            ReviewID = score.ReviewID,
                            CommunicationID = score.CommunicationID,
                            ExternalID = score.ExternalID,
                            ReviewScore = score.ReviewScore,
                            ReviewBenchmark = score.ReviewBenchmark,
                            ReviewDate = score.ReviewDate,
                        };
                        scoreData.Add(scoreItem);
                    }
                }

                foreach (CommunicationReviewRemarks remark in result.Item2)
                {
                    var isExist = remarksData.Exists(x => x.ReviewID == remark.ReviewID && x.Criteria == remark.Criteria);
                    if (!isExist)
                    {
                        var remarksItem = new CommunicationReviewRemarks
                        {
                            ReviewPeriodName = remark.ReviewPeriodName,
                            Reviewer = remark.Reviewer,
                            Reviewee = remark.Reviewee,
                            RevieweeTeamName = string.Join(",", result.Item2.Where(i => i.ReviewID == remark.ReviewID)
                                                           .Where(i => i.Criteria == remark.Criteria)
                                                           .Select(j => j.RevieweeTeamName.ToString())),
                            ReviewID = remark.ReviewID,
                            CommunicationID = remark.CommunicationID,
                            ExternalID = remark.ExternalID,
                            Topic = remark.Topic,
                            Ranking = remark.Ranking,
                            MeasurementName = remark.MeasurementName,
                            Code = remark.Code,
                            Score = remark.Score,
                            Criteria = remark.Criteria,
                            AdditionalRemark = remark.AdditionalRemark,
                            Suggestion = remark.Suggestion,
                            ReviewDate =  remark.ReviewDate,
                        };
                        remarksData.Add(remarksItem);
                    }
                }


                if (result != null)
                {
                    return new CommunicationReviewReportResponseModel
                    {
                        CommunicationReviewScoreData = scoreData,
                        CommunicationReviewRemarks = remarksData
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"ReportsService | GetCommunicationReviewReportAsync: {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
            }

            return Enumerable.Empty<CommunicationReviewReportResponseModel>().FirstOrDefault();
        }
        public async Task<List<CommReviewListResponseModel>> CommunicationReviewReportListingAsync(CommunicationReviewReportRequestModel request)
        {
            return await _reportsFactory.CommunicationReviewReportListingAsync(request);
        }

        public async Task<List<CommReviewGridResponseModel>> CommunicationReviewReportGridAsync(CommunicationReviewReportRequestModel request)
        {
            return await _reportsFactory.CommunicationReviewReportGridAsync(request);
        }

    }
}
