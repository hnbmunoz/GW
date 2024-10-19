using MediatR;
using Microsoft.Extensions.Configuration;
using MLAB.PlayerEngagement.Core.Constants;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Application.Services;

public class CampaignCustomEventSettingService : ICampaignCustomEventSettingService
{
    private readonly IMediator _mediator;
    private readonly ILogger<CampaignCustomEventSettingService> _logger;
    private readonly IMainDbFactory _mainDbFactory;
    public CampaignCustomEventSettingService(IMediator mediator
                                            , IConfiguration configuration
                                            , IMainDbFactory mainDbFactory
                                            , ILogger<CampaignCustomEventSettingService> logger)
    {
        _mediator = mediator;
        _logger = logger;
        _mainDbFactory = mainDbFactory;
    }

    public async Task<bool> CheckExistingCampaignCustomEventSettingByFilterAsync(CampaignCustomEventSettingRequestModel request)
    {
        try
        {
            var result = await _mainDbFactory
                .ExecuteQueryMultipleAsync<Int32, CampaignCustomEventSettingModel>
                (   DatabaseFactories.PlayerManagementDB,
                    StoredProcedures.USP_GetCampaignCustomEventSettingByFilter, new
                    {
                        CustomEventName = string.IsNullOrEmpty(request.CustomEventName) ? null : request.CustomEventName,
                        FromDate = string.IsNullOrEmpty(request.DateFrom) ? null : DateTime.Parse(request.DateFrom).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                        ToDate = string.IsNullOrEmpty(request.DateTo) ? null : DateTime.Parse(request.DateTo).ToUniversalTime().AddHours(8).ToString("yyyy-MM-dd HH:mm"),
                        PageSize = request.PageSize,
                        OffsetValue = request.OffsetValue,
                        SortColumn = string.IsNullOrEmpty(request.SortColumn) ? "CampaignEventSettingId" : request.SortColumn,
                        SortOrder = string.IsNullOrEmpty(request.SortOrder) ? "Asc" : request.SortOrder
                    }

                ).ConfigureAwait(false);

            return (result.Item1.FirstOrDefault() > 0); // ? true : false;
        }
        catch (Exception ex)
        {
            string errorDetail = ex.Message;
            return false;
        }
    }
}
