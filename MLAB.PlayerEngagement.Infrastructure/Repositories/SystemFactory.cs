using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Infrastructure.Utilities;
using Newtonsoft.Json;
using System.Data;
using MLAB.PlayerEngagement.Core.Enum;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Response;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Responses;
using MLAB.PlayerEngagement.Core.Models.Option.Request;
using MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Response;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Request;
using MLAB.PlayerEngagement.Core.Models.SkillsMapping.Request;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Udt;
using MLAB.PlayerEngagement.Core.Models.PostChatSurvey.Response;
using MLAB.PlayerEngagement.Core.Models.System.Codelist.Topic.Request;
using MLAB.PlayerEngagement.Core.Models.Survey;
using MLAB.PlayerEngagement.Core.Entities;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Core.Models.CampaignDashboard;
using MLAB.PlayerEngagement.Infrastructure.Communications;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Response;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request;
using MLAB.PlayerEngagement.Core.Models.System.Response;
using System.Data.SqlClient;

namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class SystemFactory : ISystemFactory
{

    private readonly IMainDbFactory _mainDbFactory;
    private readonly ILogger<SystemFactory> _logger;

    #region Constructor
    public SystemFactory(IMainDbFactory mainDbFactory, ILogger<SystemFactory> logger)
    {
        _mainDbFactory = mainDbFactory;
        _logger = logger;
    }
    #endregion

    public async Task<bool> AddOperatorAsync(int operatorId, string operatorName, int status, int createdBy, List<BrandTypeModel> brandType, List<BrandCurrencyTypeModel> currencyType)
    {


        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | AddOperatorAsync - [operatorId: {operatorId}, operatorName: {operatorName}, status: {status}, createdBy: {createdBy}, brandType: {JsonConvert.SerializeObject(brandType)}, brandCurrencyType: {JsonConvert.SerializeObject(currencyType)}]");

            DataTable brandTypeTable = DataTableConverter.ToDataTable(brandType);
            DataTable brandCurrencyTypeTable = DataTableConverter.ToDataTable(currencyType);

            await _mainDbFactory.ExecuteAsync(DatabaseFactories.MLabDB,StoredProcedures.Usp_AddOperator, new
            {
                OperatorId = operatorId,
                OperatorName = operatorName,
                Status = status,
                CreatedBy = createdBy,
                BrandType = brandTypeTable,
                BrandCurrencyType = brandCurrencyTypeTable
            }).ConfigureAwait(false);


            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | AddOperatorAsync : [Exception] - {ex.Message}");
            return false;

        }
    }

    public async Task<List<BrandInfoModel>> GetAllBrandAsync(long? userId, long? platformId)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<BrandInfoModel>
                            (   DatabaseFactories.MLabDB,StoredProcedures.Usp_GetBrand, new
                                {
                                    UserId = userId,
                                    PlatformID = platformId
                                }

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllBrandAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<BrandInfoModel>().ToList();
    }
    public async Task<List<VIPLevelModel>> GetAllVIPLevelAsync(long? userId)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<VIPLevelModel>
                            (
                               DatabaseFactories.MLabDB,StoredProcedures.Usp_GetAllVIPLevel, new
                                {
                                    UserId = userId
                                }

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllVIPLevelAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<VIPLevelModel>().ToList();
    }

    public async Task<List<VIPLevelModel>> GetAllVIPLevelByBrandAsync(long? userId, string brandId)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<VIPLevelModel>
                            (
                               DatabaseFactories.MLabDB, StoredProcedures.Usp_GetAllVIPLevelByBrand, new
                               {
                                    UserId = userId,
                                    BrandId = Int32.Parse(brandId)
                               }

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllVIPLevelByBrandAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<VIPLevelModel>().ToList();
    }

    public async Task<List<OperatorInfoModel>> GetAllOperatorAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<OperatorInfoModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetOperator,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllOperatorAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<OperatorInfoModel>().ToList();
    }


    public async Task<List<CaseTypeModel>> GetCaseTypeListAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CaseTypeModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetAllCaseType,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetCaseTypeListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CaseTypeModel>().ToList();
    }

    public async Task<Tuple<List<OperatorBrandModel>, List<BrandCurrencyModel>>> GetOperatorBrandCurrencyByOperatorIdAsync(string operatorIds)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetOperatorBrandCurrencyByOperatorIdAsync - [operatorIds: {operatorIds}]");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<OperatorBrandModel, BrandCurrencyModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetOperatorBrandCurrencyByOperatorId,
                                null
                            ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.ToList(), result.Item2.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetOperatorBrandCurrencyByOperatorIdAsync : [Exception] - {ex.Message}");
            return Tuple.Create(Enumerable.Empty<OperatorBrandModel>().ToList(), Enumerable.Empty<BrandCurrencyModel>().ToList());
        }
    }
    public async Task<Tuple<List<OperatorModel>, List<BrandModel>, List<BrandCurrencyModel>>> GetOperatorByIdAsync(int operatorId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetOperatorByIdAsync - [operatorId: {operatorId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<OperatorModel, BrandModel, BrandCurrencyModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetOperatorById, new
                                {
                                    OperatorId = operatorId
                                }

                            ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.ToList(), result.Item2.ToList(), result.Item3.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetOperatorByIdAsync : [Exception] - {ex.Message}");
            return Tuple.Create(Enumerable.Empty<OperatorModel>().ToList(), Enumerable.Empty<BrandModel>().ToList(), Enumerable.Empty<BrandCurrencyModel>().ToList());
        }

    }

    public async Task<List<OperatorBrandModel>> GetOperatorListByFilterAsync(int operatorId, string operatorName, int brandId, string brandName)
    {

        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetOperatorListByFilterAsync - [operatorId: {operatorId}, operatorName: {operatorName}, brandId: {brandId}, brandName: {brandName}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<OperatorBrandModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetOperatorListByFilter, new
                                {
                                    OperatorId = operatorId,
                                    OperatorName = operatorName,
                                    BrandId = brandId,
                                    BrandName = brandName
                                }

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetOperatorListByFilterAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<OperatorBrandModel>().ToList();

    }

    public async Task<bool> UpdateOperatorAsync(int operatorId, string operatorName, int status, int updatedBy, List<BrandTypeModel> brandType, List<BrandCurrencyTypeModel> brandCurrencyType)
    {

        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | UpdateOperatorAsync - [operatorId: {operatorId}, operatorName: {operatorName}, status: {status}, updatedBy: {updatedBy}, brandType: {JsonConvert.SerializeObject(brandType)}, brandCurrencyType: {JsonConvert.SerializeObject(brandCurrencyType)}]");

            DataTable brandTypeTable = DataTableConverter.ToDataTable(brandType);
            DataTable brandCurrencyTypeTable = DataTableConverter.ToDataTable(brandCurrencyType);

            await _mainDbFactory.ExecuteAsync(DatabaseFactories.MLabDB,StoredProcedures.Usp_UpdateOperator, new
            {
                OperatorId = operatorId,
                OperatorName = operatorName,
                Status = status,
                UpdatedBy = updatedBy,
                BrandType = brandTypeTable,
                BrandCurrencyType = brandCurrencyTypeTable

            }).ConfigureAwait(false);

            return true;

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | UpdateOperatorAsync : [Exception] - {ex.Message}");
            return false;

        }
    }

    public async Task<List<CurrencyModel>> GetAllCurrencyAsync(long? userId)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CurrencyModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetAllCurrency, new
                                {
                                    UserId = userId
                                }

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllCurrencyAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CurrencyModel>().ToList();
    }

    public async Task<Tuple<List<Int64>, List<string>>> GetBrandExistingLists(string brandIds, string brandNames)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetBrandExistingLists - [brandIds: {brandIds}, brandNames: {brandNames}]");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<Int64, string>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetBrandExistingList, new
                                {
                                    BrandIds = brandIds,
                                    BrandNames = brandNames,
                                }
                            ).ConfigureAwait(false);
            return Tuple.Create(result.Item1.ToList(), result.Item2.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetBrandExistingLists : [Exception] - {ex.Message}");
            return Tuple.Create(Enumerable.Empty<Int64>().ToList(), Enumerable.Empty<string>().ToList());
        }
    }

    public async Task<List<FieldTypeModel>> GetAllFieldTypeAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<FieldTypeModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_GetAllFieldType,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllFieldTypeAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<FieldTypeModel>().ToList();
    }

    public async Task<List<TopicModel>> GetAllTopicAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<TopicModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_GetAllTopic,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllTopicAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<TopicModel>().ToList();
    }

    public async Task<List<MessageTypeModel>> GetAllMessageTypeAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<MessageTypeModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetAllMessageType, new
                                {
                                    MessageTypeName = null as string,
                                    MessageTypeStatus = null as string
                                }
                            ).ConfigureAwait(false);
            return result.ToList();


        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllMessageTypeAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<MessageTypeModel>().ToList();
    }

    public async Task<bool> ValidateSubtopicName(string subtopicName, long subtopicId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateSubtopicName - [subtopicName: {subtopicName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateSubtopicName,
                                 new
                                 {
                                     SubtopicName = subtopicName,
                                     SubtopicId = subtopicId,
                                 }

                            ).ConfigureAwait(false);
            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateSubtopicName : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<int> ValidateCodelistName(string codelistName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateCodelistName - [codelistName: {codelistName}]");

            var result = await _mainDbFactory
                         .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_GetCodeListId,
                                 new
                                 {
                                     FilterValue = codelistName
                                 }
                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateCodelistName : [Exception] - {ex.Message}");
        }
        return 0;
    }

    public async Task<int> ValidateCodelistTypeName(string codelistTypeName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateCodelistTypeName - [codelistTypeName: {codelistTypeName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_GetCodeListTypeId,
                                 new
                                 {
                                     FilterValue = codelistTypeName
                                 }
                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateCodelistTypeName : [Exception] - {ex.Message}");
        }
        return 0;
    }

    public async Task<bool> GetTopicByNameAsync(string topicName, int caseTypeId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetTopicByNameAsync - [topicName: {topicName}], caseTypeId: {caseTypeId}]");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<int>(
                                   DatabaseFactories.MLabDB,StoredProcedures.Usp_GetTopicByName, new
                                   {
                                       TopicName = topicName,
                                       CaseTypeId = caseTypeId
                                   }
                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetTopicByNameAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<List<CodeListTypeModel>> GetAllCodeListTypeAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CodeListTypeModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_GetAllCodeTypeList, null

                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetAllCodeListTypeAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CodeListTypeModel>().ToList();
    }

    public async Task<bool> ValidateSurveyQuestion(ValidateSurveyQuestionModel surveyQuestion)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateSurveyQuestion - [surveyQuestion: {JsonConvert.SerializeObject(surveyQuestion)}]");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<int>(
                                   DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateSurveyQuestion, new
                                   {
                                       @SurveyQuestionName = surveyQuestion.SurveyQuestionName
                                   }
                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateSurveyQuestion : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ValidateSurveyQuestionAnswer(ValidateSurveyQuestionAnswerModel surveyQuestionAnswer)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateSurveyQuestionAnswer - [surveyQuestionAnswer: {JsonConvert.SerializeObject(surveyQuestionAnswer)}]");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<int>(
                                   DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateSurveyQuestionAnswers, new
                                   {
                                       @SurveyQuestionAnswer = surveyQuestionAnswer.AnswerName,
                                       @SurveyQuestionId = surveyQuestionAnswer.QuestionId
                                   }
                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateSurveyQuestionAnswer : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ValidateSurveyTemplateName(ValidateSurveyTemplateNameModel surveyTemplateName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateSurveyTemplateName - [surveyTemplateName: {JsonConvert.SerializeObject(surveyTemplateName)}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateSurveyTemplate, new
                                {
                                    @SurveyTemplateName = surveyTemplateName.SurveyTemplateName
                                }

                            ).ConfigureAwait(false);
            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateSurveyTemplateName : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ValidateSurveyTemplateQuestion(ValidateSurveyTemplateQuestionModel surveyTemplateQuestion)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateSurveyTemplateQuestion - [surveyTemplateQuestion: {JsonConvert.SerializeObject(surveyTemplateQuestion)}]");

            var result = await _mainDbFactory.ExecuteQuerySingleOrDefaultAsync<int>(
                                   DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateSurveyTemplateQuestions, new
                                   {
                                       @SurveyTemplateQuestionId = surveyTemplateQuestion.QuestionId,
                                       @SurveyTemplateId = surveyTemplateQuestion.TemplateId
                                   }
                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateSurveyTemplateQuestion : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<int> ValidateMessageTypeNameAsync(string messageTypeName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateMessageTypeNameAsync - [messageTypeName: {messageTypeName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateMessageType,
                                 new
                                 {
                                     MessageTypeName = messageTypeName
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateMessageTypeNameAsync : [Exception] - {ex.Message}");
        }

        return 0;
    }

    public async Task<int> ValidateMessageStatusNameAsync(string messageStatusName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateMessageStatusNameAsync - [messageStatusName: {messageStatusName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateMessageStatus,
                                 new
                                 {
                                     MessageStatusName = messageStatusName
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateMessageStatusNameAsync : [Exception] - {ex.Message}");
        }
        return 0;
    }

    public async Task<int> ValidateFeedbackTypeNameAsync(string feedbackTypeName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateFeedbackTypeNameAsync - [feedbackTypeName: {feedbackTypeName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateFeedbackType,
                                 new
                                 {
                                     FeedbackTypeName = feedbackTypeName
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateFeedbackTypeNameAsync : [Exception] - {ex.Message}");
        }
        return 0;
    }

    public async Task<int> ValidateFeedbackCategoryNameAsync(string feedbackCategory)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateFeedbackCategoryNameAsync - [feedbackCategory: {feedbackCategory}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateFeedbackCategory,
                                 new
                                 {
                                     FeedbackCategoryName = feedbackCategory
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateFeedbackCategoryNameAsync : [Exception] - {ex.Message}");
        }
        return 0;
    }

    public async Task<int> ValidateFeedbackAnswerNameAsync(string feedbackResponseName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateFeedbackAnswerNameAsync - [feedbackResponseName: {feedbackResponseName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateFeedbackAnswer,
                                 new
                                 {
                                     FeedbackResponseName = feedbackResponseName
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateFeedbackAnswerNameAsync : [Exception] - {ex.Message}");
        }

        return 0;

    }

    public async Task<int> ValidateMessageResponseNameAsync(string messageResponseName)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateMessageResponseNameAsync - [messageResponseName: {messageResponseName}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateMessageResponse,
                                 new
                                 {
                                     MessageResponseName = messageResponseName
                                 }

                            ).ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateMessageResponseNameAsync : [Exception] - {ex.Message}");
        }
        return 0;
    }

    public async Task<bool> DeactivateSurveyQuestion(int SurveyQuestionId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | DeactivateSurveyQuestion - [SurveyQuestionId: {SurveyQuestionId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_DeactivateSurveyQuestion, new
                                {
                                    Id = SurveyQuestionId
                                }

                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | DeactivateSurveyQuestion : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> DeactivateSurveyTemplate(int SurveyTemplateId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | DeactivateSurveyTemplate - [SurveyTemplateId: {SurveyTemplateId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_DeactivateSurveyTemplate, new
                                {
                                    Id = SurveyTemplateId
                                }

                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | DeactivateSurveyTemplate : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<SystemLookupsResponseModel> GetSystemLookupsAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory}| GetSystemLookupsAsync | Start Time : {DateTime.Now}");
            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<
                LookupModel // Standard look up model
                >(DatabaseFactories.MLabDB,StoredProcedures.USP_GetSystemLookups, null , 16).ConfigureAwait(false);

            var resultList = result.ToList();
            _logger.LogInfo($"{Factories.SystemFactory}| GetSystemLookupsAsync | End Time : {DateTime.Now}");
            return new SystemLookupsResponseModel()
            {

                Brands = resultList[0].ToList(),
                PlayerStatuses = resultList[1].ToList(),
                Currencies = resultList[2].ToList(),
                MarketingChannels = resultList[3].ToList(),
                RiskLevels = resultList[4].ToList(),

                VIPLevels = resultList[5].ToList(),
                MessageResponses = resultList[6].ToList(),
                MessageTypes = resultList[7].ToList(),
                MessageStatuses = resultList[8].ToList(),
                Countries = resultList[9].ToList(),
                SignUpPortals = resultList[10].ToList(),
                PaymentGroups = resultList[11].ToList(),
                FieldTypes = resultList[12].ToList(),
                CaseTypes = resultList[13].ToList(),
                CodeLists = resultList[14].ToList(),
                MobileVerificationStatus = resultList[15].ToList(),
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetSystemLookupsAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<SystemLookupsResponseModel>().FirstOrDefault();
    }


    public async Task<List<CaseTypeOptionModel>> GetCaseTypeOptionList()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<CaseTypeOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetCaseTypeOptionList, null
                ).ConfigureAwait(false);

            return result.ToList();

        }
        catch (Exception ex)
        {

            _logger.LogError($"{Factories.SystemFactory} | GetCaseTypeOptionList : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<CaseTypeOptionModel>().ToList();
    }

    public async Task<List<TopicOptionModel>> GetTopicOptionList()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<TopicOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetTopicOptionList, null
                ).ConfigureAwait(false);

            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetTopicOptionList : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<TopicOptionModel>().ToList();
    }
    public async Task<List<LookupModel>> GetTopicOptionByBrandId(long brandId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetTopicOptionByBrandId - [brandId: {brandId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<LookupModel>(
                    DatabaseFactories.MLabDB, StoredProcedures.USP_GetTopicByBrand, new { brandId = brandId }
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetTopicOptionByBrandId : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<SubtopicOptionModel>> GetSubtopicOptionById(int topicId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetSubtopicOptionById - [topicId: {topicId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<SubtopicOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetSubtopicOptionById, new { topicId = topicId }
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetSubtopicOptionById : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<SubtopicOptionModel>().ToList();
    }

    public async Task<List<MessageTypeOptionModel>> GetMessageTypeOptionList(string channelTypeId)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<MessageTypeOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetMessageTypeOptionList,
                    new
                    {
                        ChannelTypeId = channelTypeId
                    }
            ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetMessageTypeOptionList : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<MessageTypeOptionModel>().ToList();
    }

    public async Task<List<MessageStatusOptionModel>> GetMessageStatusOptionById(int messageTypeId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetMessageStatusOptionById - [messageTypeId: {messageTypeId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<MessageStatusOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetMessageStatusOptionById, new { messageTypeId = messageTypeId }
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetMessageStatusOptionById : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<MessageStatusOptionModel>().ToList();
    }

    public async Task<List<MessageResponseOptionModel>> GetMessageResponseOptionById(int messageStatusId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetMessageResponseOptionById - [messageStatusId: {messageStatusId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<MessageResponseOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetMessageResponseOptionById, new { messageStatusId = messageStatusId }
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetMessageResponseOptionById : [Exception] - {ex.Message}");

        }

        return Enumerable.Empty<MessageResponseOptionModel>().ToList();
    }

    public async Task<List<FeedbackTypeOptionModel>> GetFeedbackTypeOptionList(string platform)
    {
        try
        {

            var result = await _mainDbFactory.ExecuteQueryAsync<FeedbackTypeOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetFeedbackTypeOptionList, null
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} {(platform != null ? "| " + platform : "")} | GetFeedbackTypeOptionList : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<FeedbackTypeOptionModel>().ToList();
    }

    public async Task<List<FeedbackCategoryOptionModel>> GetFeedbackCategoryOptionById(int feedbackTypeId, string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} {(platform != null ? "| " + platform : "")} | GetFeedbackCategoryOptionById - [feedbackTypeId: {feedbackTypeId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<FeedbackCategoryOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetFeedbackCategoryOptionById, new { feedbackTypeId = feedbackTypeId }
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} {(platform != null ? "| " + platform : "")} | GetFeedbackCategoryOptionById : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<FeedbackCategoryOptionModel>().ToList();
    }

    public async Task<List<FeedbackAnswerOptionModel>> GetFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} {(platform != null ? "| " + platform : "")} | GetFeedbackAnswerOptionById - [feedbackAnswerOptionById: {JsonConvert.SerializeObject(request)}]");
            var result = await _mainDbFactory.ExecuteQueryAsync<FeedbackAnswerOptionModel>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetFeedbackAnswerOptionById, new
            {
                FeedbackTypeId = string.IsNullOrEmpty(request.FeedbackTypeId) ? (int?)null : Int32.Parse(request.FeedbackTypeId),
                FeedbackCategoryId = string.IsNullOrEmpty(request.FeedbackCategoryId) ? (int?)null : Int32.Parse(request.FeedbackCategoryId),
                FeedbackFilter = string.IsNullOrEmpty(request.FeedbackFilter) ? null : request.FeedbackFilter
            }).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} {(platform != null ? "| " + platform : "")} | GetFeedbackAnswerOptionById : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<FeedbackAnswerOptionModel>().ToList();
    }

    public async Task<SurveyTemplateResponse> GetCommunicationSurveyQuestionAnswers(int campaignId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetCommunicationSurveyQuestionAnswers - [campaignId: {campaignId}]");
            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<SurveyTemplateInfoResponse, SurveyQuestionResponse, SurveyQuestionAnswerResponse>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetCommunicationSurveyQuestionAnswers, new
                    {
                        CampaignId = campaignId
                    }
                ).ConfigureAwait(false);


            return new SurveyTemplateResponse()
            {
                SurveyTemplate = result.Item1.FirstOrDefault(),
                SurveyQuestions = result.Item2.ToList(),
                SurveyQuestionAnswers = result.Item3.ToList()
            };

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetCommunicationSurveyQuestionAnswers : [Exception] - {ex.Message}");
            return Enumerable.Empty<SurveyTemplateResponse>().FirstOrDefault();
        }
    }

    public async Task<List<MessageStatusResponseModel>> GetMessageStatusResponseListAsync(int campaignId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetMessageStatusResponseListAsync - [campaignId: {campaignId}]");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<MessageStatusResponseModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_GetCampaignMessageStatusResponseList, new
                                {
                                    CampaignId = campaignId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetMessageStatusResponseListAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<MessageStatusResponseModel>().ToList();
    }



    public async Task<List<MasterReferenceModel>> GetMasterReferenceList(string masterReferenceId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetMasterReferenceList - [masterReferenceId: {masterReferenceId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<MasterReferenceModel>(
                 DatabaseFactories.MLabDB,StoredProcedures.USP_GetMasterReferenceList, new
                 {
                     MasterReferenceId = masterReferenceId,
                     MasterReferenceIsParent = null as string
                 }
             ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetMasterReferenceList : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<MasterReferenceModel>().ToList();
    }

    public async Task<List<CurrencyModel>> GetCurrencyCodeAsync()
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CurrencyModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetAllCurrency,
                                null
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetCurrencyCodeAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CurrencyModel>().ToList();
    }

    public async Task<List<UserGridCustomDisplayResponseModel>> LoadUserGridCustomDisplayAsync(long userId, string module)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | LoadUserGridCustomDisplayAsync - [userId: {userId}] - [module: {module}] ");
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<UserGridCustomDisplayResponseModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_LoadUserGridCustomDisplay, new
                                {
                                    UserId = userId,
                                    Module = module
                                }
                            ).ConfigureAwait(false);
            _logger.LogInfo($"{Factories.SystemFactory} | LoadUserGridCustomDisplayAsync - [Result: {JsonConvert.SerializeObject(result)}]");
            return result.ToList();
          
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | LoadUserGridCustomDisplayAsync : [Exception] - {ex.Message}");
        }
        return null;
    }

    public async Task<Tuple<List<CurrencyFilterModel>, long>> GetCurrencyByFilterAsync(PlayerConfigurationRequestModel filter)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<CurrencyFilterModel, Int64>
            (DatabaseFactories.MLabDB,StoredProcedures.USP_GetPlayerConfigCodeListDetails, new
            {
                PlayerConfigurationTypeId = PlayerConfigurationTypes.CurrencyTypeId,
                PlayerConfigurationId = filter.PlayerConfigurationId,
                PlayerConfigurationName = filter.PlayerConfigurationName,
                PlayerConfigurationCode = filter.PlayerConfigurationCode,
                PageSize = filter.PageSize,
                OffsetValue = filter.OffsetValue,
                SortColumn = filter.SortColumn,
                SortOrder = filter.SortOrder,
                PlayerConfigurationICoreId = filter.PlayerConfigurationICoreId,

            }).ConfigureAwait(false);

            return Tuple.Create(result.Item1.ToList(), result.Item2.FirstOrDefault());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.PlayerConfigurationFactory} | GetCurrencyByFilterAsync : [Exception] - {JsonConvert.SerializeObject(ex)}");
            return Tuple.Create(Enumerable.Empty<CurrencyFilterModel>().ToList(), Enumerable.Empty<Int64>().FirstOrDefault());
        }
    }

    public async Task<List<LookupModel>> GetSkillsByLicenseIdAsync(string LicenseId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetSkillsByLicenseIdAsync - [LicenseId: {LicenseId}]");

            var result = await _mainDbFactory.ExecuteQueryAsync<LookupModel>(
                 DatabaseFactories.MLabDB,StoredProcedures.USP_GetSkillsByLicenseId, new
                 {
                     LicenseId = LicenseId,
                 }
             ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetSkillsByLicenseIdAsync : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<PostChatSurveyLookupsResponseModel> GetPCSLookupsAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetPCSLookupsAsync - [Request]");
            _logger.LogInfo($"{Factories.SystemFactory}| GetPCSLookupsAsync | Start Time : {DateTime.Now}");
            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<dynamic>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetPCSLookups, null, 4).ConfigureAwait(false);
            var resultList = result.ToList();
            List<LookupModel> licenses = new List<LookupModel>();
            List<LookupModel> skills = new List<LookupModel>();
            List<LicenseResponseModel> licenseByBrandMessageType = new List<LicenseResponseModel>(); 
            List<SkillsResponseModel> skillsByLicense = new List<SkillsResponseModel>();

            for (int i = 0; i < resultList.Count; i++)
            {
                var dictionaries = DynamicConverter.ConvertToDictionaries(resultList[i].Select(item => item));

                List<Action> actions = new List<Action>
                {
                    () =>
                    {
                        var licensesModels = DynamicConverter.ConvertToModels<LookupModel>(dictionaries);
                        licenses.AddRange(licensesModels);
                    },
                    () =>
                    {
                        var skillsModels = DynamicConverter.ConvertToModels<LookupModel>(dictionaries);
                        skills.AddRange(skillsModels);
                    },
                    () =>
                    {
                        var licenseByBrandMessageTypeModels = DynamicConverter.ConvertToModels<LicenseResponseModel>(dictionaries);
                        licenseByBrandMessageType.AddRange(licenseByBrandMessageTypeModels);
                    },
                    () =>
                    {
                        var skillsByLicenseModels = DynamicConverter.ConvertToModels<SkillsResponseModel>(dictionaries);
                        skillsByLicense.AddRange(skillsByLicenseModels);
                    },
                    // Add more actions as needed for other resultList indices
                };

                // Execute the action based on the index
                actions[i].Invoke();
            }
            _logger.LogInfo($"{Factories.SystemFactory}| GetPCSLookupsAsync | End Time : {DateTime.Now}");

            return new PostChatSurveyLookupsResponseModel()
            {
                Licenses = licenses,
                Skills = skills,
                LicenseByBrandMessageType = licenseByBrandMessageType,
                SkillsByLicense = skillsByLicense
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetPCSLookupsAsync : [Exception] - {ex.Message}");
        }

        return new PostChatSurveyLookupsResponseModel();
    }

    public async Task<List<GetTopicOrderResponseModel>> GetTopicOrderAsync()
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetTopicOrderAsync - [Request]");

            var result = await _mainDbFactory.ExecuteQueryAsync<GetTopicOrderResponseModel>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetTopicOrder, null).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetTopicOrderAsync : [Exception] - {ex.Message}");

        }
        return Enumerable.Empty<GetTopicOrderResponseModel>().ToList();
    }

    public async Task<bool> TogglePostChatSurveyAsync(PostChatSurveyToggleRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | TogglePostChatSurveyAsync - [PostChatSurveyId: {request.PostChatSurveyId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_TogglePostChatSurvey, new
                                {
                                    @PostChatSurveyId = request.PostChatSurveyId,
                                    @IsActive = request.IsActive
                                }

                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | TogglePostChatSurveyAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ToggleSkillAsync(SkillToggleRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ToggleSkillAsync - [Id: {request.Id}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                        (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ToggleSkill, new
                                {
                                    @Id = request.Id,
                                    @IsActive = request.IsActive
                                }

                            ).ConfigureAwait(false);

            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | TogglePostChatSurveyAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<bool> ValidateSkillAsync(ValidateSkillRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidateSkillAsync - [LicenseId: {request.LicenseId}], [SkillId: {request.SkillId}]");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<int>
                (
                    DatabaseFactories.MLabDB,StoredProcedures.USP_ValidateSkill, new
                    {
                        Id = request.Id,
                        LicenseId = request.LicenseId,
                        SkillId = request.SkillId
                    }

                ).ConfigureAwait(false);

            return result == 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidateSkillAsync : [Exception] - {ex.Message}");
        }

        return false;
    }

    public async Task<Tuple<PostChatSurveyResponseModel, List<SkillsUdtModel>>> GetPostChatSurveyByIdAsync(PostChatSurveyIdRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetPostChatSurveyByIdAsync - [PostChatSurveyId: {request.PostChatSurveyId}]");

            var result = await _mainDbFactory.ExecuteQueryMultipleAsync<PostChatSurveyResponseModel, SkillsUdtModel>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetPostChatSurveyById, new
            {
                PostChatSurveyId = request.PostChatSurveyId
            }).ConfigureAwait(false);

            return Tuple.Create(result.Item1.FirstOrDefault(), result.Item2.ToList());
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetPostChatSurveyByIdAsync : [Exception] - {ex.Message}");
        }
        return Tuple.Create(Enumerable.Empty<PostChatSurveyResponseModel>().First(), Enumerable.Empty<SkillsUdtModel>().ToList());
    }

    public async Task<List<GetSubtopicOrderUdtViewModel>> GetSubtopicOrderAsync(int topicId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetSubtopicOrderAsync - [Request]");

            var result = await _mainDbFactory.ExecuteQueryAsync<GetSubtopicOrderUdtViewModel>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetSubtopicOrder, new { @TopicId = topicId }).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetSubtopicOrderAsync : [Exception] - {ex.Message}");

        }
        return Enumerable.Empty<GetSubtopicOrderUdtViewModel>().ToList();
    }

    public async Task<bool> UpdateSubtopicStatusAsync(long subTopicId, string userId, bool isActive)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | UpdateSubtopicStatusAsync - [subTopicId: {subTopicId}], [isActive: {isActive}]");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<int>
                (
                    DatabaseFactories.MLabDB,StoredProcedures.USP_UpdateSubtopicStatus, new
                    {
                        SubTopicId = subTopicId,
                        UserId = userId,
                        IsActive = isActive
                    }

                ).ConfigureAwait(false);

            return result == 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | UpdateSubtopicStatusAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<bool> UpdateTopicStatusAsync(UpdateTopicStatusRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | Start |UpdateTopicStatusAsync - [topicId: {request.TopicId}], [isActive: {request.IsActive}]");

            var result = await _mainDbFactory
                .ExecuteQuerySingleOrDefaultAsync<int>
                (
                    DatabaseFactories.MLabDB,StoredProcedures.USP_UpdateTopicStatus, new
                    {
                        @TopicId = request.TopicId,
                        @IsActive = request.IsActive,
                        @UserId = request.UserId
                    }

                ).ConfigureAwait(false);

            _logger.LogInfo($"{Factories.SystemFactory} | Success | UpdateTopicStatusAsync - result: {result}");

            return result == 0;
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | UpdateTopicStatusAsync : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<bool> ValidatePostChatSurveyQuestionID(ValidatePostChatSurveyQuestionIDModel questionModel)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | ValidatePostChatSurveyQuestionID - [questionId: {questionModel.QuestionId}]");

            var result = await _mainDbFactory
                        .ExecuteQuerySingleOrDefaultAsync<int>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.USP_ValidatePostChatSurveyQuestionID,
                                 new
                                 {
                                     QuestionId = questionModel.QuestionId,
                                     PostChatSurveyId = questionModel.PostChatSurveyId,
                                     SkillId = questionModel.SkillId
                                 }

                            ).ConfigureAwait(false);
            return Convert.ToBoolean(result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | ValidatePostChatSurveyQuestionID : [Exception] - {ex.Message}");
        }
        return false;
    }

    public async Task<List<GetTopicOptionsReponse>> GetTopicOptionsAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<GetTopicOptionsReponse>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetTopicOptions, null
                ).ConfigureAwait(false);

            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetTopicOptions : [Exception] - {ex.Message}");
            return Enumerable.Empty<GetTopicOptionsReponse>().ToList();
        }
    }

    public async Task<List<TopicLanguageOptionModel>> GetTopicOptionsByCodeAsync(string languageCode, long caseTypeId)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<TopicLanguageOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetTopicOptionsByCode, new
                    {
                        LanguageCode = languageCode,
                        CaseTypeId = caseTypeId
                    }
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetTopicOptionsByCodeAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<TopicLanguageOptionModel>().ToList();
    }

    public async Task<List<SubtopicLanguageOptionModel>> GetSubtopicOptionsByIdAsync(long topicLanguageId)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<SubtopicLanguageOptionModel>(
                    DatabaseFactories.MLabDB,StoredProcedures.USP_GetSubtopicOptionsById, new
                    {
                        TopicLanguageId = topicLanguageId
                    }
                ).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetSubtopicOptionsByIdAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<SubtopicLanguageOptionModel>().ToList();
    }

    public async Task<List<PCSCommunicationProviderOptionResponseModel>> GetPCSCommunicationProviderOptionAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<PCSCommunicationProviderOptionResponseModel>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetPCSCommunicationProviderOption, null).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetPCSCommunicationProviderOptionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<PCSCommunicationProviderOptionResponseModel>().ToList();
    }

    public async Task<List<PCSCommunicationSummaryActionResponseModel>> GetPCSCommunicationSummaryActionAsync()
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<PCSCommunicationSummaryActionResponseModel>(DatabaseFactories.MLabDB,StoredProcedures.USP_GetPCSCommunicationSummaryAction, null).ConfigureAwait(false);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetPCSCommunicationSummaryActionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<PCSCommunicationSummaryActionResponseModel>().ToList();
    }

    public async Task<List<CurrencyModel>> GetCurrencyWithNullableRestrictionAsync(long? userId)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CurrencyModel>
                            (
                                DatabaseFactories.MLabDB,StoredProcedures.Usp_GetCurrency,
                                new
                                {
                                    UserId = userId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetCurrencyWithNullableRestrictionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CurrencyModel>().ToList();
    }

    public async Task<List<LookupModel>> GetRemAgentsByUserAccessAsync(long? userId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetRemAgentsByUserAccessAsync : [UserId] - {userId}");

            var result = await _mainDbFactory
                        .ExecuteQueryAsync<LookupModel>
                            (
                                DatabaseFactories.PlayerManagementDB, StoredProcedures.USP_GetRemAgentByAccess,
                                new
                                {
                                    UserId = userId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetRemAgentsByFilterAsync : [UserId] - {userId} |  [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<LookupModel>> GetDateByOptionList()
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetDateByOptionList");

            var result = await _mainDbFactory.ExecuteQueryAsync<LookupModel>(
                 DatabaseFactories.MLabDB,
                 StoredProcedures.USP_GetDateByOptionList, new
                 { }
             ).ConfigureAwait(false);

            return result.ToList();
        }
        catch(Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetDateByOptionList : [Exception] - {ex.Message}");
        }

        return Enumerable.Empty<LookupModel>().ToList();
    }

    public async Task<List<CountryListResponse>> GetCountryWithAccessRestrictionAsync(long? userId)
    {
        try
        {
            var result = await _mainDbFactory
                        .ExecuteQueryAsync<CountryListResponse>
                            (
                                DatabaseFactories.MLabDB, StoredProcedures.Usp_GetCountry,
                                new
                                {
                                    UserId = userId
                                }
                            ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetCountryWithAccessRestrictionAsync : [Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CountryListResponse>().ToList();
    }

    public async Task<StaffPerformaneSettingResponseModel> GetStaffPermormanceSettingList(StaffPerformanceSettingRequestModel request)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} | GetStaffPermormanceSettingList : [Request] - {JsonConvert.SerializeObject(request)}");

            var result = await _mainDbFactory
                        .ExecuteQueryMultipleAsync<StaffPerformanceSettingDetails, int>
                            (
                                DatabaseFactories.MLabDB, StoredProcedures.USP_GetStaffPermormanceSettingList,
                                new
                                {
                                    SortOrder = request.SortOrder,
                                    SortColumn = request.SortColumn,
                                    OffsetValue = request.OffsetValue,
                                    PageSize = request.PageSize
                                }
                            ).ConfigureAwait(false);
            return new StaffPerformaneSettingResponseModel()
            {
                StaffPerformanceSettingList = result.Item1.ToList(),
                RowCount = result.Item2.FirstOrDefault()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} | GetStaffPermormanceSettingList : [Exception] - {ex.Message}");
        }

        return new StaffPerformaneSettingResponseModel()
        {
            StaffPerformanceSettingList = Enumerable.Empty<List<StaffPerformanceSettingDetails>>().FirstOrDefault(),
            RowCount = 0
        };
    }

    public async Task<StaffPerformanceInfoResponseModel> GetStaffPerformanceInfoAsync(int id)
    {
        _logger.LogInfo($"{Factories.SystemFactory} | GetStaffPerformanceInfoAsync : [Request] - {id}");
        try
        {
            var result = await _mainDbFactory
                      .ExecuteQueryAsync<StaffPerformanceInfoResponseModel>
                          (
                              DatabaseFactories.MLabDB, StoredProcedures.USP_GetStaffPerformanceInfo,
                              new
                              {
                                  Id = id
                              }
                          ).ConfigureAwait(false);
            return result.FirstOrDefault();

        }
        catch (Exception ex )
        {
            _logger.LogError($"{Factories.SystemFactory} | GetStaffPerformanceInfoAsync : [Exception] - {ex.Message}");
            return null;
        }

       
    }

    public async Task<bool> UpsertReviewPeriodAsync(UpsertReviewPeriodRequestModel request)
    {        
        _logger.LogInfo($"{Factories.SystemFactory} | UpsertReviewPeriodAsync : [Request] - {JsonConvert.SerializeObject(request)}");
        try
        {            
            var upsertCaseCommAnnotation = await _mainDbFactory.ExecuteQueryAsync<bool>(DatabaseFactories.MLabDB, StoredProcedures.USP_UpsertCommunicationReviewPeriod, new
            {
                CommunicationReviewPeriodId = request.CommunicationReviewPeriodId,
                CommunicationReviewPeriodName = request.CommunicationReviewPeriodName,
                CommunicationStartRangeFrom = request.RangeStart,
                CommunicationStartRangeTo = request.RangeEnd,
                ValidationPeriod = request.ValidationPeriod,
                Status = request.Status,
                UserId = request.UserId,
               
            }).ConfigureAwait(false);

            return upsertCaseCommAnnotation.FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.CaseManagementFactory} | UpsertReviewPeriodAsync : [Exception] - {ex.Message}");
            return false;
        }
    }

    public async Task<List<GetAppConfigSettingByApplicationIdResponseModel>> GetAppConfigSettingByApplicationIdAsync(int ApplicationId)
    {
        try
        {
            _logger.LogInfo($"{Factories.SystemFactory} |request parameter: {ApplicationId} | GetAppConfigSettingByApplicationIdAsync");
            var result = await _mainDbFactory
            .ExecuteQueryAsync<GetAppConfigSettingByApplicationIdResponseModel>
                (
                    DatabaseFactories.MLabDB, StoredProcedures.USP_GetAppConfigSettingByApplicationId,
                    new
                    {
                        ApplicationId = ApplicationId
                    }
                ).ConfigureAwait(false);
            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError($"{Factories.SystemFactory} |request parameter : {ApplicationId} | GetAppConfigSettingByApplicationIdAsync : [Exception] - {ex}");
            return Enumerable.Empty<GetAppConfigSettingByApplicationIdResponseModel>().ToList();
        }
    }
}