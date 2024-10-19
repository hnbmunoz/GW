using MediatR;
using MLAB.PlayerEngagement.Application.Queries;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Option;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Request;
using MLAB.PlayerEngagement.Core.Response;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace.Response;
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
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Response;
using MLAB.PlayerEngagement.Core.Models.System.StaffPerformanceSetting.Request;
using MLAB.PlayerEngagement.Core.Models.System.Response;

namespace MLAB.PlayerEngagement.Application.Services;

public class SystemService : ISystemService
{
    private readonly IMediator _mediator;
    private readonly ISystemFactory _systemFactory;
    public SystemService(IMediator mediator, ISystemFactory systemFactory)
    {
        _mediator = mediator;
        _systemFactory = systemFactory;
    }

    public async Task<List<AllCurrencyResponse>> GetAllCurrenciesAsync(long? userId)
    {
        var getAllCurrencyQuery = new GetAllCurrencyQuery();
        getAllCurrencyQuery.UserId = userId;
        var results = await _mediator.Send(getAllCurrencyQuery);

        return results;
    }

    public async Task<Tuple<bool, string>> AddOperatorAsync(OperatorRequest operatorDetail)
    {
        var operators = await _systemFactory.GetAllOperatorAsync();
        bool isOperatorExists = operators.Exists(x=>x.OperatorId == operatorDetail.OperatorId || x.OperatorName == operatorDetail.OperatorName);

        if (isOperatorExists)
        {
            return Tuple.Create(false, "Unable to process, Operator ID or Operator Name already exists. If you want to edit an existing operator please find them on search page");
        }

        var brandIds = String.Join(",", operatorDetail.Brands.Where(y => y.CreateStatus == 0).Select(x => x.Id));
        var brandNames = String.Join(",", operatorDetail.Brands.Where(y => y.CreateStatus == 0).Select(x => x.Name));
        var exisitingBrands = await _systemFactory.GetBrandExistingLists(brandIds, brandNames);

        if (exisitingBrands.Item1.Any())
        {
            string errorBrandIds = $"Unable to proceed, Brand ID or Brand Name already exist. If you want to edit an existing brand please find them from search page";
            return Tuple.Create(false, errorBrandIds);
        }

        if (exisitingBrands.Item2.Any())
        {
            string errorBrandNames = $"Unable to proceed, Brand ID or Brand Name already exist. If you want to edit an existing brand please find them from search page";
            return Tuple.Create(false, errorBrandNames);
        }

        var brandTypes = BuildBrandTypes(operatorDetail);
        var brandCurrencyTypes = BuildBrandCurrencyTypes(operatorDetail);
        var isSuccess = await _systemFactory.AddOperatorAsync(
               operatorDetail.OperatorId,
               operatorDetail.OperatorName,
               operatorDetail.OperatorStatus,
               operatorDetail.CreatedBy,
               brandTypes,
               brandCurrencyTypes
            );

        if (!isSuccess)
        {
            return Tuple.Create(false, "Problem Inserting the operator");
        }

        return Tuple.Create(true, "Success");
    }

    public async Task<Tuple<bool, string>> UpdateOperatorAsync(OperatorRequest operatorDetail)
    {
        var brandTypes = BuildBrandTypes(operatorDetail);
        var brandCurrencyTypes = BuildBrandCurrencyTypes(operatorDetail);

        var brandIds = String.Join(",", operatorDetail.Brands.Where(y => y.CreateStatus == 0).Select(x => x.Id));
        var brandNames = String.Join(",", operatorDetail.Brands.Where(y => y.CreateStatus == 0).Select(x => x.Id));

        var exisitingBrands = await _systemFactory.GetBrandExistingLists(brandIds, brandNames);

        if (exisitingBrands.Item1.Any())
        {
            string errorBrandIds = $"Unable to proceed, Brand ID or Brand Name already exist. If you want to edit an existing brand please find them from search page";
            return Tuple.Create(false, errorBrandIds);
        }

        if (exisitingBrands.Item2.Any())
        {
            string errorBrandNames = $"Unable to proceed, Brand ID or Brand Name already exist. If you want to edit an existing brand please find them from search page";
            return Tuple.Create(false, errorBrandNames);
        }


        var isSuccess = await _systemFactory.UpdateOperatorAsync(
               operatorDetail.OperatorId,
               operatorDetail.OperatorName,
               operatorDetail.OperatorStatus,
               operatorDetail.CreatedBy,
               brandTypes,
               brandCurrencyTypes
            );

        if (!isSuccess)
        {
            return Tuple.Create(false, "Problem Inserting the operator");
        }

        return Tuple.Create(true, "Success");

    }

    public async Task<List<OperatorListResponse>> GetOperatorListByFilterAsync(int operatorId, string operatorName, int brandId, string brandName)
    {
        List<OperatorListResponse> response = new List<OperatorListResponse>();


        var result = await _systemFactory.GetOperatorListByFilterAsync(operatorId, operatorName, brandId, brandName);

        var operators = result.Select(x => new {
             OperatorId = x.OperatorId,
             OperatorName = x.OperatorName,
             Status = x.Status
        }).Distinct().ToList();

        foreach(var item in operators)
        {
            List<BrandListResponse> brandResponse = new List<BrandListResponse>();
            var brands = result.Where(x => x.OperatorId == item.OperatorId).ToList();
            foreach(OperatorBrandModel detail in brands)
            {
                var brandItem = new BrandListResponse
                {
                    Id = detail.BrandId,
                    Name = detail.BrandName
                };
                brandResponse.Add(brandItem);

            }
            var operatorResponse = new OperatorListResponse
            {
                OperatorId = item.OperatorId,
                OperatorName = item.OperatorName,
                OperatorStatus = item.Status,
                Brands = brandResponse
            };

            response.Add(operatorResponse);
        }

        return response;
    }

    public async Task<Tuple<bool, string>> GetBrandExistingListAsync(string brandIds, string brandNames)
    {
        var exisitingBrands = await _systemFactory.GetBrandExistingLists(brandIds, brandNames);

        if (exisitingBrands.Item1.Any())
        {
            string errorBrandIds = $"Unable to proceed, Brand ID or Brand Name already exist. If you want to edit an existing brand please find them from search page";
            return Tuple.Create(false, errorBrandIds);
        }

        if (exisitingBrands.Item2.Any())
        {
            string errorBrandNames = $"Unable to proceed, Brand ID or Brand Name already exist. If you want to edit an existing brand please find them from search page";
            return Tuple.Create(false, errorBrandNames);
        }
        return Tuple.Create(true, "");
    }

    public async Task<OperatorResponse> GetOperatorByIdAsync(int operatorId)
    {
        var result = await _systemFactory.GetOperatorByIdAsync(operatorId);
        var operatorDetail = result.Item1.FirstOrDefault();
        var response = new OperatorResponse();

        if (operatorDetail != null)
        {
            response.OperatorId = operatorDetail.OperatorId;
            response.OperatorName = operatorDetail.OperatorName;
            response.OperatorStatus = operatorDetail.Status;
            response.Brands = BuildBrandResponse(result.Item2, result.Item3, false);
            response.CreatedBy = operatorDetail.CreatedBy;
        }

        return response;

    }

    public async Task<List<OperatorResponse>> GetOperatorDetailsAsync(string operatorIds)
    {
        List<OperatorResponse> operatorList = new List<OperatorResponse>();

        if (!String.IsNullOrEmpty(operatorIds))
        {
            var lisOfOperatorIds = operatorIds.Split(',');

            foreach (string operatorId in lisOfOperatorIds)
            {
                var result = await _systemFactory.GetOperatorByIdAsync(Convert.ToInt32(operatorId));
                var operatorDetail = result.Item1.FirstOrDefault();
                var response = new OperatorResponse();

                if (operatorDetail != null)
                {
                    response.OperatorId = operatorDetail.OperatorId;
                    response.OperatorName = operatorDetail.OperatorName;
                    response.OperatorStatus = operatorDetail.Status;
                    response.Brands = BuildBrandResponse(result.Item2, result.Item3, false);
                    response.CreatedBy = operatorDetail.CreatedBy;
                }

                operatorList.Add(response);
            }
        }

        return operatorList;
    }

    public async Task<List<BrandInfoModel>> GetAllBrandAsync(long? userId, long? platformId)
    {
        var results = await _systemFactory.GetAllBrandAsync(userId, platformId);
        return results;
    }

    public async Task<List<VIPLevelModel>> GetAllVIPLevelAsync(long? userId)
    {
        var results = await _systemFactory.GetAllVIPLevelAsync(userId);
        return results;
    }

    public async Task<List<VIPLevelModel>> GetAllVIPLevelByBrandAsync(long? userId, string brandId)
    {
        var results = await _systemFactory.GetAllVIPLevelByBrandAsync(userId, brandId);
        return results;
    }

    public async Task<List<OperatorInfoModel>> GetAllOperatorAsync()
    {
        var results = await _systemFactory.GetAllOperatorAsync();
        return results;
    }

    public async Task<List<CaseTypeModel>> GetCaseTypeListAsync()
    {
        var results = await _systemFactory.GetCaseTypeListAsync();
        return results;
    }


    private static List<BrandResponse> BuildBrandResponse(List<BrandModel> brandList, List<BrandCurrencyModel> brandCurrencies, bool  isActiveFiltered)
    {
        List<BrandResponse> brands = new List<BrandResponse>();

        foreach (BrandModel brand in brandList)
        {
            var brandItem = new BrandResponse
                  {
                       Id = brand.BrandId,
                       Name = brand.BrandName,
                       Status = brand.Status,
                       Currencies = BuildCurrencyResponse(brand.BrandId, brandCurrencies, isActiveFiltered),
                       CreateStatus = 1
                  };

            brands.Add(brandItem);
        }

        return brands;
    }

    private static List<BrandCurrencyResponse> BuildCurrencyResponse(int brandId, List<BrandCurrencyModel> brandCurrencies, bool isActiveFiltered)
    {
        List<BrandCurrencyResponse> currencies = new List<BrandCurrencyResponse>();

        var items = brandCurrencies.Where(x => x.BrandId == brandId);

        foreach (BrandCurrencyModel item in items)
        {
           if (!String.IsNullOrEmpty(item.CurrencyName))
              {
                  if (isActiveFiltered)
                  {
                      if (item.CurrencyStatus == 1)
                      {
                          var currencyItem = new BrandCurrencyResponse
                          {
                              Id = item.CurrencyId,
                              Name = item.CurrencyName,
                              Status = item.CurrencyStatus
                          };

                          currencies.Add(currencyItem);
                      }
                }
                  else
                  {
                    var currencyItem = new BrandCurrencyResponse
                    {
                        Id = item.CurrencyId,
                        Name = item.CurrencyName,
                        Status = item.CurrencyStatus
                    };

                    currencies.Add(currencyItem);
                }
                
              }
        }

        return currencies;
    }
    private static List<BrandTypeModel> BuildBrandTypes(OperatorRequest operatorDetail)
    {
        List<BrandTypeModel> brandTypeModels = new List<BrandTypeModel>();
        var brandRequest = operatorDetail.Brands;

        foreach (BrandRequest brand in brandRequest)
        {
            var brandType = new BrandTypeModel
            {
                OperatorId = operatorDetail.OperatorId,
                BrandId = brand.Id,
                BrandName = brand.Name,
                Status = brand.Status,
                CreatedBy = operatorDetail.CreatedBy,
                UpdatedBy = 0
            };
            brandTypeModels.Add(brandType);
        }
        return brandTypeModels;
    }

    private static List<BrandCurrencyTypeModel> BuildBrandCurrencyTypes(OperatorRequest operatorDetail)
    {
        List<BrandCurrencyTypeModel> brandCurrencyModels = new List<BrandCurrencyTypeModel>();
        var brandRequest = operatorDetail.Brands;

        foreach (BrandRequest brand in brandRequest)
        {
            foreach(CurrencyRequest currency in brand.Currencies)
            {
                var brandCurrency = new BrandCurrencyTypeModel
                {
                BrandId = brand.Id,
                CurrencyId = currency.Id,
                CreatedBy = operatorDetail.CreatedBy,
                UpdatedBy = 0,
                CurrencyStatus = currency.Status
                };
                brandCurrencyModels.Add(brandCurrency);

            }         
        }
        return brandCurrencyModels;
    }

    public async Task<List<FieldTypeModel>> GetAllFieldTypeAsync()
    {
        var results = await _systemFactory.GetAllFieldTypeAsync();
        return results;
    }

    public async Task<List<TopicModel>> GetAllTopicAsync()
    {
        var results = await _systemFactory.GetAllTopicAsync();
        return results;
    }
    public async Task<List<MessageTypeModel>> GetAllMessageTypeAsync()
    {
        var results = await _systemFactory.GetAllMessageTypeAsync();
        return results;
    }
    public async Task<Tuple<int, string>> ValidateCodelistTypeNameAsync(string codeListTypeName)
    {
        var results = await _systemFactory.ValidateCodelistTypeName(codeListTypeName);
        if (results != 0)
        {
            string errorMessage = $"Codelist Type Already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<Tuple<int, string>> ValidateCodelistNameAsync(string codeListName)
    {
        var results = await _systemFactory.ValidateCodelistName(codeListName);
        if (results != 0)
        {
            string errorMessage = $"Codelist Name Already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<bool> ValidateSubtopicNameAsync(string subtopicName, long subtopicId)
    {
        var results = await _systemFactory.ValidateSubtopicName(subtopicName, subtopicId);

        return results;
    }

    public async Task<bool> GetTopicByNameAsync(string topicName, int caseTypeId)
    {
        var results = await _systemFactory.GetTopicByNameAsync(topicName,caseTypeId);
        return results;
    }

    public async Task<List<CodeListTypeModel>> GetAllCodeListTypeAsync()
    {
        var results = await _systemFactory.GetAllCodeListTypeAsync();
        return results;
    }

    public async Task<bool> ValidateSurveyQuestionAsync(ValidateSurveyQuestionModel request)
    {
        return await _systemFactory.ValidateSurveyQuestion(request);
    }

    public async Task<bool> ValidateSurveyQuestionAnswerAsync(ValidateSurveyQuestionAnswerModel request)
    {
        return await _systemFactory.ValidateSurveyQuestionAnswer(request);
    }

    public async Task<bool> ValidateSurveyTemplateNameAsync(ValidateSurveyTemplateNameModel request)
    {
        return await _systemFactory.ValidateSurveyTemplateName(request);
    }

    public async Task<bool> ValidateSurveyTemplateQuestionAsync(ValidateSurveyTemplateQuestionModel request)
    {
        return await _systemFactory.ValidateSurveyTemplateQuestion(request);
    }

    public async Task<Tuple<int, string>> ValidateMessageResponseNameAsync(string messageResponseName)
    {
        var results = await _systemFactory.ValidateMessageResponseNameAsync(messageResponseName);
        if (results != 0)
        {
            string errorMessage = $"Message Response Already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<Tuple<int, string>> ValidateMessageTypeNameAsync(string messageTypeName)
    {
        var results = await _systemFactory.ValidateMessageTypeNameAsync(messageTypeName);
        if (results != 0)
        {
            string errorMessage = $"Message Type already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<Tuple<int, string>> ValidateMessageStatusNameAsync(string messageStatusName)
    {
        var results = await _systemFactory.ValidateMessageStatusNameAsync(messageStatusName);
        if (results != 0)
        {
            string errorMessage = "Message Status already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<Tuple<int, string>> ValidateFeedbackTypeNameAsync(string feedbackTypeName)
    {
        var results = await _systemFactory.ValidateFeedbackTypeNameAsync(feedbackTypeName);
        if (results != 0)
        {
            string errorMessage =$"Feedback Type Already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<Tuple<int, string>> ValidateFeedbackCategoryNameAsync(string feedbackCategory)
    {
        var results = await _systemFactory.ValidateFeedbackCategoryNameAsync(feedbackCategory);
        if (results != 0)
        {
            string errorMessage = $"Feedback Category Already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<Tuple<int, string>> ValidateFeedbackAnswerNameAsync(string feedbackResponseName)
    {
        var results = await _systemFactory.ValidateFeedbackAnswerNameAsync(feedbackResponseName);
        if (results != 0)
        {
            string errorMessage = $"Feedback Answer Already exist";
            return Tuple.Create(409, errorMessage);
        }
        return Tuple.Create(200, "");
    }

    public async Task<bool> DeactivateSurveyQuestion(int SurveyQuestionId)
    {
        var results = await _systemFactory.DeactivateSurveyQuestion(SurveyQuestionId);

        return results;
    }

    public async Task<bool> DeactivateSurveyTemplate(int SurveyTemplateId)
    {
        var results = await _systemFactory.DeactivateSurveyTemplate(SurveyTemplateId);

        return results;
    }

    public async Task<List<CaseTypeOptionModel>> GetCaseTypeOptionList()
    {
        return await _systemFactory.GetCaseTypeOptionList();
    }

    public async Task<List<TopicOptionModel>> GetTopicOptionList()
    {
        return await _systemFactory.GetTopicOptionList();
    }
    public async Task<List<LookupModel>> GetTopicOptionByBrandId(long brandId)
    {
        return await _systemFactory.GetTopicOptionByBrandId(brandId);
    }
    public async Task<List<SubtopicOptionModel>> GetSubtopicOptionById(int topicId)
    {
        return await _systemFactory.GetSubtopicOptionById(topicId);
    }

    public async Task<List<MessageTypeOptionModel>> GetMessageTypeOptionList(string channelTypeId)
    {
        return await _systemFactory.GetMessageTypeOptionList(channelTypeId);
    }

    public async Task<List<MessageStatusOptionModel>> GetMessageStatusOptionById(int messageTypeId)
    {
        return await _systemFactory.GetMessageStatusOptionById(messageTypeId);
    }

    public async Task<List<MessageResponseOptionModel>> GetMessageResponseOptionById(int messageStatusId)
    {
        return await _systemFactory.GetMessageResponseOptionById(messageStatusId);
    }

    public async Task<List<FeedbackTypeOptionModel>> GetFeedbackTypeOptionList(string platform)
    {
        return await _systemFactory.GetFeedbackTypeOptionList(platform);
    }

    public async Task<List<FeedbackCategoryOptionModel>> GetFeedbackCategoryOptionById(int feedbackTypeId, string platform)
    {
        return await _systemFactory.GetFeedbackCategoryOptionById(feedbackTypeId, platform);
    }

    public async Task<List<FeedbackAnswerOptionModel>> GetFeedbackAnswerOptionById(FeedbackAnswerOptionByIdRequestModel request, string platform)
    {
        return await _systemFactory.GetFeedbackAnswerOptionById(request, platform);
    }

    public async Task<SystemLookupsResponseModel> GetSystemLookupsAsync()
    {
        var results = await _systemFactory.GetSystemLookupsAsync();
        return results;
    }

    public async Task<SurveyTemplateResponse> GetCommunicationSurveyQuestionAnswers(int campaignId)
    {
        var result = await _systemFactory.GetCommunicationSurveyQuestionAnswers(campaignId);

        return result;
    }

    public async Task<List<MessageStatusResponseModel>> GetMessageStatusResponseListAsync(int campaignId)
    {
        var results = await _systemFactory.GetMessageStatusResponseListAsync(campaignId);
        return results;
    }

    public async Task<List<MasterReferenceModel>> GetMasterReferenceList(string masterReferenceId)
    {
        var results = await _systemFactory.GetMasterReferenceList(masterReferenceId);
        return results;
    }

    public async Task<List<CurrencyModel>> GetCurrencyCodeAsync()
    {
        var results = await _systemFactory.GetCurrencyCodeAsync();
        return results;
    }

    public async Task<List<UserGridCustomDisplayResponseModel>> LoadUserGridCustomDisplayAsync(long userId, string module)
    {
        var results = await _systemFactory.LoadUserGridCustomDisplayAsync(userId, module);
        return results;
    }

    public async Task<CurrencyListResponseModel> GetCurrencyByFilterAsync(PlayerConfigurationRequestModel filter)
    {
        var result = await _systemFactory.GetCurrencyByFilterAsync(filter);

        var currencyList = new CurrencyListResponseModel()
        {
            CurrencyList = result.Item1,
            RecordCount = result.Item2
        };
        return currencyList;
    }

    public async Task<List<LookupModel>> GetSkillsByLicenseIdAsync(string LicenseId)
    {
        var results = await _systemFactory.GetSkillsByLicenseIdAsync(LicenseId);
        return results;
    }

    public async Task<PostChatSurveyLookupsResponseModel> GetPCSLookupsAsync()
    {
        var results = await _systemFactory.GetPCSLookupsAsync();
        return results;
    }

    public async Task<bool> ValidatePostChatSurveyQuestionID(ValidatePostChatSurveyQuestionIDModel questionModel)
    {
        var results = await _systemFactory.ValidatePostChatSurveyQuestionID(questionModel);
        return results;
    }
    public async Task<List<GetTopicOrderResponseModel>> GetTopicOrderAsync()
    {
        var results = await _systemFactory.GetTopicOrderAsync();
        return results;
    }
        
    public async Task<bool> TogglePostChatSurveyAsync(PostChatSurveyToggleRequestModel request)
    {
        var results = await _systemFactory.TogglePostChatSurveyAsync(request);

        return results;
    }

    public async Task<PostChatSurveyResponseModel> GetPostChatSurveyByIdAsync(PostChatSurveyIdRequestModel request)
    {
        var result = await _systemFactory.GetPostChatSurveyByIdAsync(request);

        var pcsList = new PostChatSurveyResponseModel()
        {
            PostChatSurveyId = result.Item1.PostChatSurveyId,
            BrandId = result.Item1.BrandId,
            MessageType = result.Item1.MessageType,
            MessageTypeId  = result.Item1.MessageTypeId,
            License = result.Item1.License,
            SkillsList = result.Item2,
            QuestionId = result.Item1.QuestionId,
            QuestionMessage = result.Item1.QuestionMessage,
            QuestionMessageEN = result.Item1.QuestionMessageEN,
            FreeText = result.Item1.FreeText,
            Status = result.Item1.Status,
            SurveyId = result.Item1.SurveyId,
            CsatTypeId = result.Item1.CsatTypeId,
        };

        return pcsList;
    }

    public async Task<bool> ToggleSkillAsync(SkillToggleRequestModel request)
    {
        var results = await _systemFactory.ToggleSkillAsync(request);

        return results;
    }

    public async Task<bool> ValidateSkillAsync(ValidateSkillRequestModel request)
    {
        var results = await _systemFactory.ValidateSkillAsync(request);

        return results;
    }

    public async Task<List<GetSubtopicOrderUdtViewModel>> GetSubtopicOrderAsync(int topicId)
    {
        var results = await _systemFactory.GetSubtopicOrderAsync(topicId);
        return results;
    }

    public async Task<bool> UpdateSubtopicStatusAsync(long subTopicId, string userId, bool isActive)
    {
        var results = await _systemFactory.UpdateSubtopicStatusAsync(subTopicId, userId, isActive);

        return results;
    }

    public async Task<bool> UpdateTopicStatusAsync(UpdateTopicStatusRequestModel request)
    {
        var results = await _systemFactory.UpdateTopicStatusAsync(request);

        return results;
    }

    public async Task<List<GetTopicOptionsReponse>> GetTopicOptionsAsync()
    {
        var results = await _systemFactory.GetTopicOptionsAsync();

        return results;
    }

    public async Task<List<TopicLanguageOptionModel>> GetTopicOptionsByCodeAsync(string languageCode, long caseTypeId)
    {
        var results = await _systemFactory.GetTopicOptionsByCodeAsync(languageCode, caseTypeId);
        return results;
    }

    public async Task<List<SubtopicLanguageOptionModel>> GetSubtopicOptionsByIdAsync(long topicLanguageId)
    {
        var results = await _systemFactory.GetSubtopicOptionsByIdAsync(topicLanguageId);
        return results;
    }

    public async Task<List<PCSCommunicationProviderOptionResponseModel>> GetPCSCommunicationProviderOptionAsync()
    {
        var results = await _systemFactory.GetPCSCommunicationProviderOptionAsync();
        return results;
    }

    public async Task<List<PCSCommunicationSummaryActionResponseModel>> GetPCSCommunicationSummaryActionAsync()
    {
        var results = await _systemFactory.GetPCSCommunicationSummaryActionAsync();
        return results;
    }

    public async Task<List<CurrencyModel>> GetCurrencyWithNullableRestrictionAsync(long? userId)
    {
        var results = await _systemFactory.GetCurrencyWithNullableRestrictionAsync(userId);
        return results;
    }

    public async Task<List<LookupModel>> GetRemAgentsByUserAccessAsync(long? userId)
    {
        var results = await _systemFactory.GetRemAgentsByUserAccessAsync(userId);
        return results;
    }

    public async Task<List<LookupModel>> GetDateByOptionList()
    {
        var results = await _systemFactory.GetDateByOptionList();
        return results;
    }

    public async Task<List<CountryListResponse>> GetCountryWithAccessRestrictionAsync(long? userId)
    {
        var results = await _systemFactory.GetCountryWithAccessRestrictionAsync(userId);
        return results;
    }

    public async Task<StaffPerformaneSettingResponseModel> GetStaffPermormanceSettingList(StaffPerformanceSettingRequestModel request)
    {
        var results = await _systemFactory.GetStaffPermormanceSettingList(request);
        return results;
    }

    public async Task<StaffPerformanceInfoResponseModel> GetStaffPerformanceInfoAsync(int id)
    {
        var results = await _systemFactory.GetStaffPerformanceInfoAsync(id);
        return results;
    }

    public async Task<bool> UpsertReviewPeriodAsync(UpsertReviewPeriodRequestModel request)
    {
        var results = await _systemFactory.UpsertReviewPeriodAsync(request);

        return results;
    }

    public async Task<List<GetAppConfigSettingByApplicationIdResponseModel>> GetAppConfigSettingByApplicationIdAsync(int ApplicationId)
    {
        var results = await _systemFactory.GetAppConfigSettingByApplicationIdAsync(ApplicationId);
        return results;
    }
}
