using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models.Reports;
using MLAB.PlayerEngagement.Core.Repositories;
using Newtonsoft.Json;


namespace MLAB.PlayerEngagement.Infrastructure.Repositories
{
    public class ReportsFactory: IReportsFactory
    {
        private readonly IMainDbFactory _mainDbFactory;
        private readonly ILogger<ReportsFactory> _logger;

        public ReportsFactory(IMainDbFactory mainDbFactory, ILogger<ReportsFactory> logger)
        {
            _mainDbFactory = mainDbFactory;
            _logger = logger;
        }

        public async Task<Tuple<List<CommunicationReviewScoreData>, List<CommunicationReviewRemarks>>> GetCommunicationReviewReportAsync(CommunicationReviewReportRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.ReportsFactory} | GetCommunicationReviewReportAsync - {JsonConvert.SerializeObject(request)}");

                var result = await _mainDbFactory
                                    .ExecuteQueryMultipleAsync<CommunicationReviewScoreData, CommunicationReviewRemarks>
                                        ( DatabaseFactories.PlayerManagementDB,
                                            StoredProcedures.USP_GetCommunicationReviewReportByFilter, new
                                            {
                                                DisplayResultsGrouping = request.DisplayResultsGrouping,
                                                RevieweeTeamIds = request.RevieweeTeamIds,
                                                RevieweeIds = request.RevieweeIds,
                                                ReviewerIds = request.ReviewerIds,
                                                CommunicationRangeStart = String.IsNullOrWhiteSpace(request.CommunicationRangeStart) ? null : request.CommunicationRangeStart.ToLocalDateTime(),
                                                CommunicationRangeEnd = String.IsNullOrWhiteSpace(request.CommunicationRangeEnd) ? null : request.CommunicationRangeEnd.ToLocalDateTime(),
                                                ReviewPeriod = request.ReviewPeriod,
                                                HasLineComments = request.HasLineComments,
                                            }

                                        ).ConfigureAwait(false);

                return Tuple.Create(result.Item1.ToList(), result.Item2.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ReportsFactory} | GetCommunicationReviewReportAsync : {ex.InnerException} Message: {ex.Message} StackTrace: {ex.StackTrace}");
            }

            return Tuple.Create(Enumerable.Empty<CommunicationReviewScoreData>().ToList(), Enumerable.Empty<CommunicationReviewRemarks>().ToList());
        }
        public async Task<List<CommReviewListResponseModel>> CommunicationReviewReportListingAsync(CommunicationReviewReportRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.CaseManagementFactory} | CommunicationReviewReportListingAsync - {JsonConvert.SerializeObject(request)} ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<CommReviewListResponseModel>
                                (DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCommunicationReviewData, new
                                    {
                                        RevieweeTeamIds = request.RevieweeTeamIds,
                                        RevieweeIds = request.RevieweeIds,
                                        ReviewerIds = request.ReviewerIds,
                                        CommunicationRangeStart = String.IsNullOrWhiteSpace(request.CommunicationRangeStart) ? null : request.CommunicationRangeStart.ToLocalDateTime(),
                                        CommunicationRangeEnd = String.IsNullOrWhiteSpace(request.CommunicationRangeEnd) ? null : request.CommunicationRangeEnd.ToLocalDateTime(),
                                        ReviewPeriod = request.ReviewPeriod,
                                        DisplayResultsGrouping = request.DisplayResultsGrouping,
                                    }

                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ReportsFactory} | CommunicationReviewReportListingAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<CommReviewListResponseModel>().ToList();
        }


        public async Task<List<CommReviewGridResponseModel>> CommunicationReviewReportGridAsync(CommunicationReviewReportRequestModel request)
        {
            try
            {
                _logger.LogInfo($"{Factories.ReportsFactory} | CommunicationReviewReportGridAsync - {JsonConvert.SerializeObject(request)} ");

                var result = await _mainDbFactory
                            .ExecuteQueryAsync<CommReviewGridResponseModel>
                                (   DatabaseFactories.PlayerManagementDB,
                                    StoredProcedures.USP_GetCommunicationReviewData, new
                                    {
                                        RevieweeTeamIds = request.RevieweeTeamIds,
                                        RevieweeIds = request.RevieweeIds,
                                        ReviewerIds = request.ReviewerIds,
                                        CommunicationRangeStart = String.IsNullOrWhiteSpace(request.CommunicationRangeStart) ? null : request.CommunicationRangeStart.ToLocalDateTime(),
                                        CommunicationRangeEnd = String.IsNullOrWhiteSpace(request.CommunicationRangeEnd) ? null : request.CommunicationRangeEnd.ToLocalDateTime(),
                                        ReviewPeriod = request.ReviewPeriod,                                        
                                        SectedIds = request.RevieweeIds,
                                        DisplayResultsGrouping = request.DisplayResultsGrouping,
                                    }

                                ).ConfigureAwait(false);

                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{Factories.ReportsFactory} | CommunicationReviewReportGridAsync : [Exception] - {ex.Message}");
            }
            return Enumerable.Empty<CommReviewGridResponseModel>().ToList();
        }
    }
}
