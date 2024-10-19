using MediatR;
using Microsoft.AspNetCore.Http;
using MLAB.PlayerEngagement.Core;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Enum;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Response;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class CampaignManagementService : ICampaignManagementService
{
    private readonly IMediator _mediator;
    private readonly ILogger<SystemService> _logger;
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly ICampaignManagementFactory _campaignManagementFactory;
    private readonly IMainDbFactory _mainDbFactory;

    public CampaignManagementService(IMediator mediator, ILogger<SystemService> logger, IMessagePublisherService messagePublisherService, ICampaignManagementFactory campaignManagementFactory, IMainDbFactory mainDbFactory)
    {
        _mediator = mediator;
        _logger = logger;
        _messagePublisherService = messagePublisherService;
        _campaignManagementFactory = campaignManagementFactory;
        _mainDbFactory = mainDbFactory;
    }
    public async Task<List<LookupModel>> GetAllCampaignAsync()
    {
        var campaigns = await _campaignManagementFactory.GetAllCampaignAsync();
        return campaigns;
    }
    public async Task<List<LookupModel>> GetCampaignByFilterAsync(CampaignLookupByFilterRequestModel request)
    {
        var campaigns = await _campaignManagementFactory.GetCampaignByFilterAsync(request);
        return campaigns;
    }

    public async Task<List<LookupModel>> GetAllCampaignStatusAsync()
    {
        var masterReferenceId = await _campaignManagementFactory.GetMasterReferenceId(MasterReferenceName.CampaignStatus.GetDescription(), true);

        var campaignType = await _campaignManagementFactory.GetMasterReferenceList(masterReferenceId, false);

        List<LookupModel> lookUp = new List<LookupModel>();

        foreach (var item in campaignType)
        {
            var lookUpModel = new LookupModel();


            lookUpModel.Value = item.MasterReferenceId;
            lookUpModel.Label = item.MasterReferenceChildName;
            
            lookUp.Add(lookUpModel);
        }
        return lookUp;
    }
    public async Task<List<LookupModel>> GetAllCampaignType()
    {
        var masterReferenceId = await _campaignManagementFactory.GetMasterReferenceId(MasterReferenceName.CampaignType.GetDescription(), true);

        var campaignType = await _campaignManagementFactory.GetMasterReferenceList(masterReferenceId, false);

        List<LookupModel> lookUp = new List<LookupModel>();

        foreach (var item in campaignType)
        {
            LookupModel lookUpModel = new LookupModel
            {
                Value = item.MasterReferenceId,
                Label = item.MasterReferenceChildName
            };
            lookUp.Add(lookUpModel);
        }
        return lookUp;
    }

    public async Task<List<LookupModel>> GetAllLeaderValidation()
    {

        var leaderValidation = await _campaignManagementFactory.GetAllLeaderValidation();
        return leaderValidation;
    }

    public async Task<List<LookupModel>> GetAllSegmentAsync()
    {
        var segmentList = await _campaignManagementFactory.GetAllSegmentAsync();
        return segmentList;
    }
    public async Task<List<CustomLookModel>> GetSegmentWithSegmentTypeNameAsync()
    {
        var segmentList = await _campaignManagementFactory.GetSegmentWithSegmentTypeNameAsync();
        return segmentList;
    }
    
    public async Task<List<LookupModel>> GetAllSurveyTemplateAsync()
    {
        var surveyTemplate = await _campaignManagementFactory.GetAllSurveyTemplateAsync();
        return surveyTemplate;
    }

    public async Task<CampaignGoalSettingListModel> GetCampaignGoalSettingByIdAsync(int campaignGoalId)
    {
        var campaignGoalSettingList = await _campaignManagementFactory.GetCampaignGoalSettingByIdAsync(campaignGoalId);
        return campaignGoalSettingList;
    }

    public async Task<List<CampaignGoalSettingListModel>> GetCampaignGoalSettingListAsync()
    {
        var campaignGoalSettingList = await _campaignManagementFactory.GetCampaignGoalSettingListAsync();
        return campaignGoalSettingList.OrderBy(a => a.CampaignSettingName).ToList();
    }

    public async Task<CampaignLookUpsResponseModel> GetCampaignGoalParametersAsync()
    {
        var campaignLookUp = await _campaignManagementFactory.GetCampaignLookUpAsync();
        var campaignLookUpList = new CampaignLookUpsResponseModel
        {
            GoalParameterPointSetting = new List<LookupModel>(),
            GoalParameterValueSetting = new List<LookupModel>(),
            AutoTaggingSettting = new List<LookupModel>()
        };

        foreach (var item in campaignLookUp.Item1)
        {
            var lookUpModel = new LookupModel
            {
                Label = item.CampaignSettingName,
                Value = item.CampaignSettingId
            };

            campaignLookUpList.GoalParameterValueSetting.Add(lookUpModel);
        }
        foreach (var item in campaignLookUp.Item2)
        {
            var lookUpModel = new LookupModel
            {
                Label = item.CampaignSettingName,
                Value = item.CampaignSettingId
            };

            campaignLookUpList.GoalParameterPointSetting.Add(lookUpModel);
        }
        foreach (var item in campaignLookUp.Item3)
        {
            var lookUpModel = new LookupModel
            {
                Label = item.CampaignSettingName,
                Value = item.CampaignSettingId
            };

            campaignLookUpList.AutoTaggingSettting.Add(lookUpModel);
        }
        return campaignLookUpList;
    }

    public async Task<List<SegmentListModel>> GetSegmentationByIdAsync(int segmentId)
    {
        var segmentation = await _campaignManagementFactory.GetSegmentationByIdAsync(segmentId);
        return segmentation;
    }

    public async Task<List<CampaignAutoTaggingListModel>> GetAutoTaggingDetailsByIdAsync(int campaignSettingId)
    {
        var autotagging = await _campaignManagementFactory.GetAutoTaggingDetailsByIdAsync(campaignSettingId);
        foreach (var item in autotagging)
        {
            item.IsActive = true;
        }
        return autotagging;
    }

    public async Task<Tuple<int, string>> ValidateCampaign(CampaignModel request)
    {
        if(!String.IsNullOrEmpty(request.CampaignName))
        {
            var campaignList = await _campaignManagementFactory.GetAllCampaignAsync();

            if (campaignList.Exists(p => String.Equals(p.Label, request.CampaignName.TrimStart().TrimEnd(), StringComparison.OrdinalIgnoreCase) && p.Value != request.CampaignId))
                return Tuple.Create(StatusCodes.Status409Conflict, "Unable to record, the Campaign Name is already exist");

        }
        return Tuple.Create(StatusCodes.Status200OK, "Success");

    }

    public async Task<List<LookupModel>> GetEligibilityTypeAsync()
    {
        var masterReferenceId = await _campaignManagementFactory.GetMasterReferenceId(MasterReferenceName.EligibilityType.GetDescription(), true);

        var eligibility = await _campaignManagementFactory.GetMasterReferenceList(masterReferenceId, false);

        var lookUp = new List<LookupModel>();

        foreach (var item in eligibility)
        {
            LookupModel lookUpModel = new LookupModel
            {
                Value = item.MasterReferenceId,
                Label = item.MasterReferenceChildName
            };

            lookUp.Add(lookUpModel);
        }

        return lookUp;
    }

    public async Task<List<LookupModel>> GetSearchFilterAsync()
    {
        var masterReferenceId = await _campaignManagementFactory.GetMasterReferenceId(MasterReferenceName.SearchActivityType.GetDescription(), true);

        var searchFilter = await _campaignManagementFactory.GetMasterReferenceList(masterReferenceId, false);

        var lookUp = new List<LookupModel>() ;

        foreach (var item in searchFilter)
        {
            LookupModel lookUpModel = new LookupModel
            {
                Value = item.MasterReferenceId,
                Label = item.MasterReferenceChildName
            };

            lookUp.Add(lookUpModel);
        }

        return lookUp;
    }

    public async Task<List<LookupModel>> GetAllCampaignBySearchFilterAsync(int searchFilterType, string searchFilterField, int campaignType)
    {
        var campaigns = await _campaignManagementFactory.GetAllCampaignBySearchFilterAsync(searchFilterType, searchFilterField, campaignType);

        return campaigns;
    }

    public async Task<List<CampaignUploadPlayerList>> GetExportCampaignUploadPlayerListAsync(UploadPlayerFilterModel request)
    {
        var result = await _campaignManagementFactory.GetExportCampaignUploadPlayerListAsync(request);
        return result;
    }

    public async Task<List<LookupModel>> GetAllCampaignCustomEventSettingNameAsync()
    {
        var result = await _campaignManagementFactory.GetAllCampaignCustomEventSettingNameAsync();
        return result;
    }

    public async Task<bool> ValidateHasPlayerInCampaignAsync(long campaignId, string campaignGuid)
    {
        var result = await _campaignManagementFactory.ValidateHasPlayerInCampaignAsync(campaignId, campaignGuid);
        return result;
    }

    public async Task<List<CampaignConfigurationSegmentModel>> GetCampaignConfigurationSegmentByIdAsync(long segmentId, long? varianceId)
    {
        var segmentList = await _campaignManagementFactory.GetCampaignConfigurationSegmentByIdAsync(segmentId, varianceId);
        return segmentList;
    }

    public async Task<List<CustomLookModel>> GetCampaignPeriodBySourceId(long sourceID)
    {
        try
        {
            var result = await _mainDbFactory.ExecuteQueryAsync<CustomLookModel
                >(DatabaseFactories.MLabDB, StoredProcedures.USP_GetCampaignPeriodBySourceId
                , new 
                {
                    SourceID = sourceID
                }).ConfigureAwait(false);

            return result.ToList();

        }
        catch (Exception ex)
        {
            _logger.LogError($"[Exception] - {ex.Message}");
        }
        return Enumerable.Empty<CustomLookModel>().ToList();
    }
}
