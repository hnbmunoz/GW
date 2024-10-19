using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Models.AgentWorkspace;
using MLAB.PlayerEngagement.Core.Services;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AgentWorkspaceController : BaseController
{
    private readonly IAgentWorkspaceService _agentWorkspaceService;
    private readonly IAuthenticationService _authService;
    private readonly IMessagePublisherService _messagePublisherService;
    private readonly ISystemService _systemService;
    private readonly IUserManagementService _userManagementService;
    public AgentWorkspaceController(IAgentWorkspaceService agentWorkspaceService, 
        IAuthenticationService authService,
        IMessagePublisherService messagePublisherService, 
        ISystemService systemService,
        IUserManagementService userManagementService)
    {
        _agentWorkspaceService = agentWorkspaceService;
        _authService = authService;
        _messagePublisherService = messagePublisherService;
        _systemService = systemService;
        _userManagementService = userManagementService;
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignPlayerListByFilterAsync([FromBody] CampaignPlayerFilterRequestModel request)
    {
        try
        {
            var result = await _messagePublisherService.GetCampaignPlayerListByFilterAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }



    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> SaveCallListNoteAsync([FromBody] CallListNoteRequestModel request)
    {
        var result = await _agentWorkspaceService.SaveCallListNote(request);

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
    public async Task<IActionResult> GetCallListNoteAsync(int callListNoteId)
    {
        try
        {
            var result = await _agentWorkspaceService.GetCallListNoteAsync(callListNoteId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> TagAgentAsync([FromBody] TagAgentRequestModel request)
    {
        var result = await _agentWorkspaceService.TaggingAgentAsync(request);

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
    public async Task<ResponseModel> DiscardPlayerAsync([FromBody] DiscardAgentRequestModel request)
    {
        var result = await _agentWorkspaceService.DiscardAgentPlayerAsync(request);

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
    public async Task<IActionResult> GetAllCampaignsAsync(int campaignType)
    {
        try
        {
            var result = await _agentWorkspaceService.GetAllCampaignListAsync(campaignType);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignAgentsAsync(int campaignId)
    {
        try
        {
            var result = await _agentWorkspaceService.GetCampaignAgentListAsync(campaignId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessageStatusResponseListAsync(int campaignId)
    {
        try
        {
            var result = await _systemService.GetMessageStatusResponseListAsync(campaignId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPlayerDepositAttemptsAsync(int campaignPlayerId)
    {
        try
        {
            var result = await _agentWorkspaceService.GetPlayerDepositAttemptListAsync(campaignPlayerId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAgentsForTagging()
    {
        try
        {
            var result = await _userManagementService.GetAgentsForTagging();
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ResponseModel> ValidateTaggingAsync([FromBody] ValidateTagAgentRequestModel request)
    {

        var isValid = await _agentWorkspaceService.ValidateTagAsync(request);
        if (!isValid)
        {
            return new ResponseModel()
            {
                Status = (int)HttpStatusCode.BadRequest,
                Message = "Unable to proceed. The player has been tagged to another user."
            };
        }
        else
        {
            return new ResponseModel();
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCampaignPlayersByFilterAsync([FromBody] CampaignPlayerFilterRequestModel request)
    {
        try
        {
            var result = await _agentWorkspaceService.GetCampaignPlayerListByFilterAsync(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<FileContentResult> ExportToCSVAsync([FromBody] CampaignPlayerFilterRequestModel request)
    {
        try
        {
            var result = await _agentWorkspaceService.GetCampaignPlayerListByFilterAsync(request);
            var userInfo = await _authService.GetUserModulePermissionAsync(Convert.ToInt32(request.UserId));

            StringBuilder sb = new StringBuilder();
            sb.Append("Call List ID,Call List Notes,Player ID,Username,Status,Brand,Currency,Country,Marketing Source,Campaign Type, Mobile Number Verification,Campaign Name,Registration Date,Deposited,FTD Amount,FTD Date,Last Deposit Date," +
                "Last Deposit Amount,Agent Name,Tagged by,Tagged Date,Primary Goal Reached,Primary Goal Count,Primary Goal Amount,Primary Goal Amount in USD,Valid Incentive Points,Valid Incentive Sourced,Valid Incentive Sourced USD," +
                "Invalid Incentive Points,Invalid Incentive Source,Invalid Incentive Source USD,Incentive Value,System Validation,Agent Validation,Agent Validation Updated,Agent Validation Notes,Leader Validation,Leader Validation Updated,Leader Justification,Leader Validation Notes," +
                "Call Evaluation Point,Call Evaluation Notes,High Deposit Amount,Last Message status,Last Call Date,Last Call Response,Call count,Contactable Call Count,Last Contactable Case Date,Total call duration,First call datetime,First call status," +
                "Second call datetime,Second call status,Last Call Date Time After 2nd,Last Call status After 2nd,First Call attempt count,Second Call attempt count,Additional Call attempt,Campaign ID,Initial Deposit Amount,Initial Deposit Date, Initial Deposit Method, Initial Deposited, Total Deposit Amount,Total Deposit Count, Deposit Attempts, Last Login Date").Append("\r\n");
            int index = 1;
            foreach (var p in result.CampaignPlayers)
            {
                string parseCallEvaluationNotes = String.Empty;
                parseCallEvaluationNotes = string.IsNullOrEmpty(p.CallEvaluationNotes)
                    ? ""
                    : p.CallEvaluationNotes.ToString().Replace("\n", string.Empty);
                string parseCallListNote = string.Empty;

                parseCallListNote = string.IsNullOrEmpty(p.CallListNote)
                    ? ""
                    : p.CallListNote.ToString().Replace("\n", string.Empty);
                sb.Append(ParseCSVRow(p, parseCallListNote, parseCallEvaluationNotes));
                sb.Append("\r\n");
                index++;
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Campaign_Players_Results.csv");
        }
        catch (Exception ex)
        {
            return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "Campaign_Players_Results.csv");
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetServiceCommunicationHistoryByFilterAsync([FromBody] ServiceCommunicationHistoryFilterRequestModel request)
    {
        try
        {
            var result = await _agentWorkspaceService.GetCommunicationHistoryByFilter(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    private static string ParseCSVRow(CampaignPlayerModel p, string callListNote, string callEvaluationNotes)
    {
        return $"{p.CallListId},{callListNote},{p.PlayerId},{p.Username},{p.Status},{p.Brand},{p.Currency},{p.Country.ReplaceComma()},{p.MarketingSource.ReplaceComma()},{p.CampaignType.ReplaceComma()},{p.MobileStatus},{p.CampaignName.ReplaceComma()},{p.RegisteredDate?.ToMlabExportDateString()},{(p.FTDDate == null ? "No" : "Yes")},{p.FTDAmount},{p.FTDDate?.ToMlabExportDateString()},{p.LastDepositDate?.ToMlabExportDateString()}," +
               $"{p.LastDepositAmount},{p.AgentName},{p.TaggedBy},{p.TaggedDate?.ToMlabExportDateString()},{(p.CampaignPrimaryGoalReached ? "Yes" : "No")},{(p.CampaignPrimaryGoalReached ? p.CampaignPrimaryGoalCount : 0)},{(p.CampaignPrimaryGoalReached ? p.CampaignPrimaryGoalAmount : 0)},{p.PrimaryGoalAmountInUSD},{p.ValidIncentivePoints},{p.ValidIncentiveSourced},{p.ValidIncentiveSourcedUSD}," +
               $"{p.InvalidIncentivePoints},{p.InvalidIncentiveSource},{p.InvalidIncentiveSourceUSD},{p.IncentiveValue},{(p.SystemValidation ? "Valid" : "Invalid")},{(p.AgentValidation ? "Valid" : "Invalid")},{(p.AgentValidationUpdated != null && p.AgentValidationUpdated == true ? "Yes" : "No")},{p.AgentValidationNotes},{(p.LeaderValidation ? "Valid" : "Invalid")},{(p.LeaderValidationUpdated != null && p.LeaderValidationUpdated == true ? "Yes" : "No")},{p.LeaderJustification},{p.LeaderValidationNotes}," +
               $"{p.CallEvaluationPoint},{callEvaluationNotes},{p.HighDepositAmount},{p.LastMessageStatus},{p.CampaignLastCallDate?.ToMlabExportDateString()},{p.LastCallResponse},{p.CallCount},{p.ContactableCallCount},{p.CampaignLastContactableCaseDate?.ToMlabExportDateString()},{p.TotalCallDuration},{p.FirstCallDatetime?.ToMlabExportDateString()},{p.FirstCallStatus}," +
               $"{p.SecondCallDatetime?.ToMlabExportDateString()},{p.SecondCallStatus},{p.LastCallDateTimeAfter2nd?.ToMlabExportDateString()},{p.LastCallStatusAfter2nd},{p.FirstCallAttemptCount},{p.SecondCallAttemptCount},{p.AdditionalCallAttempt},{p.CampaignId},{p.InitialDepositAmount},{p.InitialDepositDate?.ToMlabExportDateString()}, {p.InitialDepositMethod}, {((p.InitialDeposited != null && p.InitialDeposited == true) ? "Yes" : "No")}, {p.TotalDepositAmount},{p.TotalDepositCount}, {(p.DepositAttempts > 0 ? p.DepositAttempts : "")},{p.LastLoginDate?.ToMlabExportDateString()}";
    }
}
