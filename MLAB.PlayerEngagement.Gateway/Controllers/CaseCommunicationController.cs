using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Application.Services;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;
using MLAB.PlayerEngagement.Core.Models.CaseCommunication.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;
using MLAB.PlayerEngagement.Core.Models.RelationshipManagement.Request;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;
using System.Text;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CaseCommunicationController : BaseController
{
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly IPlayerManagementService _playerService;
    private readonly ICaseManagementService _caseManagementService;

    public CaseCommunicationController(IMessagePublisherService messagePublisherService, IPlayerManagementService playerService, ICaseManagementService caseManagementService)
    {
        _messagePublisherService = messagePublisherService;
        _playerService = playerService;
        _caseManagementService = caseManagementService;
    }

    #region CaseCommmunication
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> AddCaseCommunicationAsync([FromBody] AddCaseCommunicationRequest request)
    {

        var result = await _messagePublisherService.AddCaseCommunicationAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCaseInformationbyIdAsync([FromBody] CaseInformationRequest request)
    {

        var result = await _messagePublisherService.GetCaseInformationbyIdAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCommunicationByIdAsync([FromBody] CommunicationByIdRequest request)
    {

        var result = await _messagePublisherService.GetCommunicationByIdAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCommunicationListAsync([FromBody] CommunicationListRequest request)
    {

        var result = await _messagePublisherService.GetCommunicationListAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ChangeCaseStatusAsync([FromBody] ChangeCaseStatusRequest request)
    {

        var result = await _messagePublisherService.ChangeCaseStatusAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCommunicationSurveyAsync([FromBody] CommunicationSurveyRequest request)
    {

        var result = await _messagePublisherService.GetCommunicationSurveyAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCommunicationFeedbackListAsync([FromBody] CommunicationFeedbackListRequest request)
    {

        var result = await _messagePublisherService.GetCommunicationFeedbackListAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> UpdateCaseInformationAsync([FromBody] UpdateCaseInformationRequest request)
    {

        var result = await _messagePublisherService.UpdateCaseInformationAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCaseCampaignByIdAsync([FromBody] CaseCampaignByIdRequest request)
    {

        var result = await _messagePublisherService.GetCaseCampaignByIdAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCaseContributorByIdAsync([FromBody] CaseContributorListRequest request)
    {

        var result = await _messagePublisherService.GetCaseContributorByIdAsync(request);

        if (result)
        {
            return new ResponseModel();
        }
        else
        {
            return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateCaseCampaignPlayerAsync(string playerId, int campaignId, string brandName)
    {
        var request = new CaseCampaigndPlayerIdRequest
        {
            PlayerId = playerId,
            CampaignId = campaignId,
            BrandName = brandName
        };

        var result = await _playerService.ValidateCaseCampaignPlayerAsync(request);

        var responseModel = new ResponseModel
        {
            Status = result.Item1,
            Message = result.Item2
        };
        return responseModel;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> GetCaseCommunicationListAsync([FromBody] CaseCommunicationFilterRequest request)
    {
        var result = await _messagePublisherService.GetCaseCommunicationListAsync(request);

        return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignByCaseTypeId(int caseTypeId)
    {
        try
        {
            var result = await _caseManagementService.GetCampaignByCaseTypeId(caseTypeId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> ExportCaseCommToCsvAsync([FromBody] CaseCommunicationFilterRequest request)
    {
        try
        {
            var result = await _caseManagementService.GetCaseCommunicationListCsvAsync(request);

            StringBuilder sb = new StringBuilder();
            sb.Append("Case Type,Brand,Username,VIP Level,Currency,Case Status,Communication Id,Case Id, External Id,Campaign Name,Duration,Subject,Communication Start Date,Communication End Date,Topic,Subtopic,Message Type,Notes,Communication Owner,Communication Owner Team,Reported Date").Append("\r\n");
            int index = 1;
            foreach (var p in result)
            {
                var campaignName = p.CampaignName != null && p.CampaignName.Split(",").Length > 1 ? $"\"{p.CampaignName.Replace(",", "|")}\"" : p.CampaignName;
                var communicationOwnerTeamName = p.CommunicationOwnerTeamName != null && p.CommunicationOwnerTeamName.Split(",").Length > 1 ? $"\"{p.CommunicationOwnerTeamName}\"" : p.CommunicationOwnerTeamName;
                sb.Append($"{p.CaseType},{p.Brand},{p.UserName},{p.VIPLevel},{p.Currencies},{p.CaseStatus},{p.CaseCommunicationId},{p.CaseInformatIonId},{p.ExternalCommunicationId},{campaignName},{p.Duration},{p.Subject.CsvQuoteAndReplace()},{p.CommunicationStartDate.ToMlabExportDateString()},{p.CommunicationEndDate.ToMlabExportDateString()},{p.Topic},{p.Subtopic.CsvQuoteAndReplace()},{p.MessageType},{p.Notes},{p.CommunicationOwner},{communicationOwnerTeamName}, {p.ReportedDate.ToMlabExportDateString()}");
                sb.Append("\r\n");
                index++;

            }
            var fileBytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();
            return File(fileBytes, "text/csv", $"CaseMgmt_SearchCaseResult.csv");

        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "CaseMgmt_SearchCaseResult.csv");
        }

    }
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCommunicationOwnerAsync()
    {
        var result = await _caseManagementService.GetAllCommunicationOwnerAsync();

        return Ok(result);
    }

    
    #endregion
}
