using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CallListValidation;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using Newtonsoft.Json;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CampaignDashboardFactory : ICampaignDashboardFactory
{
    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<CampaignDashboardFactory> _logger;

    public CampaignDashboardFactory(IMainDbFactory mainDbFactory, ILogger<CampaignDashboardFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }

    public async Task<CampaignSurveyAndFeedbackReportResponseModel> GetCampaignSurveyAndFeedbackReport(CampaignSurveyAndFeedbackReportRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetCampaignSurveyAndFeedbackReport - {JsonConvert.SerializeObject(request)}");
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetCampaignSurveyAndFeedbackReport | Start Time : {DateTime.Now}");
            var result = await _mainDbFactory
                                    .ExecuteQueryMultipleAsync<dynamic>
                                        (
                                            DatabaseFactories.PlayerManagementDB,
                                            StoredProcedures.USP_GetCampaignSurveyAndFeedbackReport, new
                                            {
                                                @CampaignId = request.CampaignId.ToString(),
                                                @CurrencyId = request.CurrencyId.ToString(),
                                                @RegistrationDateStart = request.RegistrationDateStart,
                                                @RegistrationDateEnd = request.RegistrationDateEnd,
                                                @TaggedDateStart = request.TaggedDateStart,
                                                @TaggedDateEnd = request.TaggedDateEnd,
                                                @IncludeDiscardTo = request.IncludeDiscardPlayerTo,
                                                @CampaignTypeId = request.CampaignTypeId,
                                            } , 4
                                        ).ConfigureAwait(false);

            var resultList = result.ToList();
            List<SurveyAndFeedbackReportSummary> reportSummary  = new List<SurveyAndFeedbackReportSummary>();
            List<LookupModel> feedbackResultSummary = new List<LookupModel>();
            List<CampaignFeedbackResultResponseModel> feedbackResult = new List<CampaignFeedbackResultResponseModel>();
            List<CampaignSurveyResultResponseModel> surveyResult =  new List<CampaignSurveyResultResponseModel>();


            for (int i = 0; i < resultList.Count; i++)
            {
                
                var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));

                List<Action> actions = new List<Action>
                {
                    () =>
                    {
                        var reportSummaryModels = DynamicConverter.ConvertToModels<SurveyAndFeedbackReportSummary>(dictionaries);
                        reportSummary.AddRange(reportSummaryModels);
                    },
                    () =>
                    {
                        var feedbackResultSummaryModels = DynamicConverter.ConvertToModels<LookupModel>(dictionaries);
                        feedbackResultSummary.AddRange(feedbackResultSummaryModels);
                    },
                    () =>
                    {
                        var feedbackResultModels = DynamicConverter.ConvertToModels<CampaignFeedbackResultResponseModel>(dictionaries);
                        feedbackResult.AddRange(feedbackResultModels);
                    },
                    () =>
                    {
                        var surveyResultModels = DynamicConverter.ConvertToModels<CampaignSurveyResultResponseModel>(dictionaries);
                        surveyResult.AddRange(surveyResultModels);
                    },
                    // Add more actions as needed for other resultList indices
                };

                // Execute the action based on the index
                actions[i].Invoke();
            }
            _logger.LogInfo($"{Factories.AgentWorkspaceFactory} | GetCampaignSurveyAndFeedbackReport | End Time : {DateTime.Now}");
            return new CampaignSurveyAndFeedbackReportResponseModel
            {
                ReportSummary = reportSummary.FirstOrDefault(),
                FeedbackResultSummary = feedbackResultSummary,
                FeedbackResult = feedbackResult,
                SurveyResult = surveyResult
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.AgentWorkspaceFactory} | GetCampaignPlayerListByFilterAsync : [Exception] - {ex.Message}");
            return new CampaignSurveyAndFeedbackReportResponseModel();
        }
    }
}
