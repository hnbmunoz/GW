using MediatR;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Segmentation;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;
using NLog.Targets;

namespace MLAB.PlayerEngagement.Application.Services;

public class SegmentationService : ISegmentationService
{
    private readonly IMediator _mediator;
    private readonly ILogger<SegmentationService> _logger;
    private readonly ISegmentationFactory _segmentationFactory;
    private readonly IMessagePublisherService _messagePublisherService;

    public SegmentationService(IMediator mediator, ILogger<SegmentationService> logger, ISegmentationFactory segmentationFactory, IMessagePublisherService messagePublisherService)
    {
        _mediator = mediator;
        _logger = logger;
        _segmentationFactory = segmentationFactory;
        _messagePublisherService = messagePublisherService;
    }

    public async Task<bool> DeactivateSegmentAsync(int SegmentId, int userId)
    {
        var results = await _segmentationFactory.DeactivateSegmentAsync(SegmentId, userId);

        return results;
    }

    public async Task<List<SegmentationFilterFieldModel>> GetSegmentConditionFieldsAsync()
    {
        var results = await _segmentationFactory.GetSegmentConditionFieldsAsync();

        return results;
    }

    public async Task<List<SegmentationFilterOperatorModel>> GetSegmentConditionOperatorsAsync()
    {
        var results = await _segmentationFactory.GetSegmentConditionOperatorsAsync();

        return results;
    }

    public async Task<SegmentationModel> GetSegmentByIdAsync(int segmentId)
    {
        var results = await _segmentationFactory.GetSegmentByIdAsync(segmentId);
        return new SegmentationModel
        {
            SegmentId = results.Item1.SegmentId,
            SegmentName = results.Item1.SegmentName,
            SegmentDescription = results.Item1.SegmentDescription,
            IsActive = results.Item1.IsActive,
            IsStatic = results.Item1.IsStatic,
            QueryForm = results.Item1.QueryForm,
            CreatedBy = results.Item1.CreatedBy,
            CreatedDate = results.Item1.CreatedDate,
            UpdatedBy = results.Item1.UpdatedBy,
            UpdatedDate = results.Item1.UpdatedDate,
            SegmentConditions = results.Item2,
            SegmentTypeId = results.Item1.SegmentTypeId,
            SegmentVariances = results.Item3,
            IsReactivated = results.Item1.IsReactivated,
            QueryFormTableau = results.Item1.QueryFormTableau,
            TableauEventQueueId = results.Item1.TableauEventQueueId,
            InputTypeId = results.Item1.InputTypeId,
            PlayerId = results.Item1.PlayerId,
        };
    }

    public async Task<bool> ValidateSegmentAsync(int? segmentId, string segmentName)
    {
        return await _segmentationFactory.ValidateSegmentAsync(segmentId, segmentName);
    }

    public async Task<List<SegmentPlayer>> TestSegmentAsync(SegmentationTestModel request)
    {
        return await _segmentationFactory.TestSegmentAsync(request);
    }

    public async Task<SegmentationTestResponseModel> TestStaticSegmentAsync(SegmentationTestModel request)
    {
        return await _segmentationFactory.TestStaticSegmentAsync(request);
    }

    public async Task<SegmentationToStaticResponseModel> ToStaticSegmentation(SegmentationToStaticModel request)
    {
        var segmentData = await _segmentationFactory.ToStaticSegmentation(request);
        return segmentData;
    }

    public async Task<List<SegmentConditionSetResponseModel>> GetSegmentConditionSetByParentIdAsync(int ParentSegmentConditionFieldId)
    {
        return await _segmentationFactory.GetSegmentConditionSetByParentIdAsync(ParentSegmentConditionFieldId);
    }

    public async Task<List<LookupModel>> GetCampaignGoalNamesByCampaignIdAsync(int CampaignId)
    {
        return await _segmentationFactory.GetCampaignGoalNamesByCampaignIdAsync(CampaignId);
    }

    public async Task<List<LookupModel>> GetVariancesBySegmentIdAsync(int SegmentId)
    {
        return await _segmentationFactory.GetVariancesBySegmentIdAsync(SegmentId);
    }

    public async Task<SegmentLookupsResponseModel> GetSegmentLookupsAsync()
    {
        var results = await _segmentationFactory.GetSegmentLookupsAsync();
        return new SegmentLookupsResponseModel
        {
            FieldList = results.Item1,
            OperatorList = results.Item2,
            SegmentList = results.Item3.Where(x => x.FieldName == "SegmentDistribution")
                .Select(x => new LookupModel { Value = x.Value,  Label = x.Label})
                .OrderBy(x => x.Label).ToList(),
            CampaignList = results.Item3.Where(x => x.FieldName == "CampaignNameWithoutDraft")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            LifecycleStageList = results.Item3.Where(x => x.FieldName == "LifeStage")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            ProductList = results.Item3.Where(x => x.FieldName == "CustomProduct")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            VendorList = results.Item3.Where(x => x.FieldName == "Vendor")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            PaymentMethodList = results.Item3.Where(x => x.FieldName == "PaymentMethod")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            BonusContextStatusList = results.Item3.Where(x => x.FieldName == "BonusContextStatus")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            BonusContextDateMappingList = results.Item3.Where(x => x.FieldName == "BonusContextDateMapping")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label }).ToList(),
            BonusCategoryList = results.Item3.Where(x => x.FieldName == "BonusCategory")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            ProductTypeList = results.Item3.Where(x => x.FieldName == "ProductType")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
            RemProfileList = results.Item3.Where(x => x.FieldName == "RemProfile")
                .Select(x => new LookupModel { Value = x.Value, Label = x.Label })
                .OrderBy(x => x.Label).ToList(),
        };

    }

    public async Task<InFileSegmentPlayerResponseModel> ValidateInFilePlayersAsync(ValidateInFileRequestModel request)
    {
        var results = await _segmentationFactory.ValidateInFilePlayersAsync(request);
        return new InFileSegmentPlayerResponseModel
        {
            ValidBrandId = results.Item1.ValidBrandId,
            ValidPlayerCount = results.Item1.ValidPlayersCnt,
            InvalidPlayerCount = results.Item1.InvalidPlayersCnt,
            DuplicatePlayerCount = results.Item1.DuplicateCnt,
            RemarksForInvalidPlayers = results.Item2,
            ValidPlayerIdList = results.Item1.ValidPlayerIdList
        };
    }
    
    public async Task<bool> TriggerVarianceDistributionAsync(TriggerVarianceDistributionRequestModel request)
    {
        var results = await _segmentationFactory.TriggerVarianceDistributionAsync(request);
        return results;

    }

    public async Task<SegmentDistributionByFilterResponseModel> GetVarianceDistributionForCSVAsync(SegmentDistributionByFilterRequestModel request)
    {
        return await _segmentationFactory.GetVarianceDistributionForCSVAsync(request);
    }

    public async Task<List<SegmentationConditionModel>> GetSegmentConditionsBySegmentIdAsync(int SegmentId)
    {
        return await _segmentationFactory.GetSegmentConditionsBySegmentIdAsync(SegmentId);
    }

    public async Task<List<LookupModel>> GetMessageStatusByCaseTypeIdAsync(string CaseTypeId)
    {
        return await _segmentationFactory.GetMessageStatusByCaseTypeIdAsync(CaseTypeId);
    }

    public async Task<List<LookupModel>> GetMessageResponseByMultipleIdAsync(string MessageStatusId)
    {
        return await _segmentationFactory.GetMessageResponseByMultipleIdAsync(MessageStatusId);
    }

    public async Task<ValidateCustomQueryResponseModel> ValidateCustomQueryAsync(string CustomQuery)
    {
        var response = new ValidateCustomQueryResponseModel()
        {
            IsValid = true,
            Message = ""
        };

        var prohibitedKeywords = await _segmentationFactory.GetSegmentCustomQueryProhibitedKeywordsAsync();

        if (!String.IsNullOrWhiteSpace(prohibitedKeywords))
        {
            var prohibitedList = prohibitedKeywords.Split(',').Select(i => i.ToLower()).ToArray();
            var customQueryTokens = CustomQuery.Split(" ").Where(s => !String.IsNullOrWhiteSpace(s)).Select(s => s.ToLower().Trim()).ToArray();
            var hasHit = customQueryTokens.Intersect(prohibitedList).Any();

            if(hasHit)
            {
                return new ValidateCustomQueryResponseModel()
                {
                    IsValid = false,
                    Message = "Unable to proceed, Prohibited SQL keywords found in the custom query"
                };
            }
        }

        return response;
    }
}
