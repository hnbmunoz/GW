using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Core.Services;
using MLAB.PlayerEngagement.Application.Responses;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using MLAB.PlayerEngagement.Core.Extensions;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement.Response;
using System.Text;
using MLAB.PlayerEngagement.Core.Models.FlyFone.Request;
using MLAB.PlayerEngagement.Core.Models.CloudTalk.Request;
using MLAB.PlayerEngagement.Core.Models.Samespace.Request;
using MLAB.PlayerEngagement.Core.Models.CaseManagement;
using MLAB.PlayerEngagement.Application.Services;
using MLAB.PlayerEngagement.Core.Models.CampaignManagement;

namespace MLAB.PlayerEngagement.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CaseManagementController : BaseController
    {
        private readonly ICaseManagementService _caseManagementService;
        private readonly IMessagePublisherService _messagePublisherService;
        public CaseManagementController(ICaseManagementService caseManagementService, IMessagePublisherService messagePublisherService)
        {
            _caseManagementService = caseManagementService;
            _messagePublisherService = messagePublisherService;
        }
       
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerCaseByIdAsync(int customerCaseId, long userId)
        {
            try
            {
                var result = await _caseManagementService.GetCustomerCaseByIdAsync(customerCaseId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerCaseCommListAsync(CustomerCaseCommunicationListRequest request)
        {
            try
            {
                var result = await _caseManagementService.GetCustomerCaseCommListAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCaseCommunicationByIdAsync(int communicationId, long userId)
        {
            try
            {
                var result = await _caseManagementService.GetCaseCommunicationByIdAsync(communicationId, userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> UpSertCustomerServiceCaseCommunicationAsync([FromBody] AddCustomerServiceCaseCommunicationRequest request)
        {

            try
            {
                var result = await _caseManagementService.UpSertCustomerServiceCaseCommunicationAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidatePlayerCaseCommunicationAsync(long mlabPlayerId)
        {
            var result = await _caseManagementService.ValidatePlayerCaseCommunicationAsync(mlabPlayerId);
            if (result != null) return Ok(result);
            return NotFound(new { message = "Player not found." });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerServiceCaseInformationByIdAsync(long caseInformationId, long userId)
        {
            var result = await _caseManagementService.GetCustomerServiceCaseInformationByIdAsync(caseInformationId, userId);
            return Ok(result);

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayersByUsernameAsync(string username, int brandId, long userId)
        {
            var result = await _caseManagementService.GetPlayersByUsernameAsync(username, brandId, userId);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayersByPlayerIdAsync(string playerId, int brandId, long userId)
        {
            var result = await _caseManagementService.GetPlayersByPlayerIdAsync(playerId, brandId, userId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeCustomerServiceCaseCommStatusAsync([FromBody] ChangeStatusCustomerServiceRequest request)
        {
            var result = await _messagePublisherService.ChangeCustomerServiceCaseCommStatusAsync(request);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<FileContentResult> GetPCSQuestionaireListCsvAsync([FromBody] PCSQuestionaireListByFilterRequestModel request)
        {
            try
            {

                var sb = new StringBuilder();

                var result = await _caseManagementService.GetPCSQuestionaireListCsvAsync(request);

                var questionHeader = result.QuestionAnswer.OrderBy(t => t.QuestionMessage).Select(t => $"\"{t.QuestionMessage}\"").Distinct();
                var questionList = string.Join(",", questionHeader);

                sb.Append($"Communication ID, Username, ExternalId, CommunicationOwner, Topic, Subtopic, Communication Start Date, {questionList}, Summary, Action").Append("\r\n");
                

                var grp = result.RecordList.GroupBy(t => new 
                {
                    t.CaseCommunicationId,
                    t.Username,
                    t.ExternalId,
                    t.CommunicationOwner,
                    t.SubtopicName,
                    t.TopicName,
                    t.CommunicationStartDate,
                    t.Summary,
                    t.Action
                }).ToList();

                foreach (var item in grp.Select(item => item.Key))
                {
                    var ids = result.RecordList.Where(t => t.CaseCommunicationId == item.CaseCommunicationId).Select(t => t.Id).ToArray();
                    var sbAnswer = new StringBuilder();

                    foreach (var question in questionHeader)
                    {
                        var answer = result.QuestionAnswer.Where(t => ids.Contains(t.Id) && $"\"{t.QuestionMessage}\"" == question && !string.IsNullOrEmpty(t.Answer)).OrderBy(t => t.QuestionMessage).Select(t => t.Answer.CsvQuoteAndReplace());

                        if (answer.Any())
                        {
                            sbAnswer.Append($"{answer.FirstOrDefault()},");
                        }
                        else
                        {
                            sbAnswer.Append(',');
                        }
                    }
                    sbAnswer.Remove(sbAnswer.Length - 1, 1);

                    sb.Append($"{item.CaseCommunicationId},{item.Username},{item.ExternalId},{item.CommunicationOwner},{item.TopicName},{item.SubtopicName},{item.CommunicationStartDate.ToString("dd-MM-yyyy HH:mm:ss")},{sbAnswer.ToString()}, {item.Summary}, {item.Action}").Append("\r\n");
                }

                var fileBytes = Encoding.UTF8.GetPreamble().Concat(Encoding.UTF8.GetBytes(sb.ToString())).ToArray();

                return File(fileBytes, "text/csv", "PCS_Q&A.csv");
            }
            catch (Exception ex)
            {
                return File(Encoding.UTF8.GetBytes(ex.ToString()), "text/csv", "PCS_Q&A.csv");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ResponseModel> UpsertChatSurveyActionAndSummary([FromBody] ChatSurveySummaryAndActionRequestModel request)
        {
            var result = await _messagePublisherService.UpsertChatSurveyActionAndSummaryAsync(request);
            if (result)
            {
                return new ResponseModel();
            }
            else
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ResponseModel> GetChatSurveyByIdAsync([FromBody] ChatSurveyByIdRequestModel request)
        {
            var result = await _messagePublisherService.GetChatSurveyByIdAsync(request);
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
        public async Task<IActionResult> GetPCSCommunicationQuestionsByIdAsync(long caseCommunicationId)
        {
            var result = await _caseManagementService.GetPCSCommunicationQuestionsByIdAsync(caseCommunicationId);
            return Ok(result);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCaseManagementPCSQuestionsByFilterAsync(CaseManagementPCSQuestionsByFilterRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.GetCaseManagementPCSQuestionsByFilterAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCaseManagementPCSCommunicationByFilterAsync(CaseManagementPCSCommunicationByFilterRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.GetCaseManagementPCSCommunicationByFilterAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerCaseCommSurveyTemplateByIdAsync(int surveyTemplateId)
        {
            var result = await _caseManagementService.GetCustomerCaseCommSurveyTemplateByIdAsync(surveyTemplateId);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCaseCommunicationFeedbackByIdAsync(int communicationId)
        {
            try
            {
                var result = await _caseManagementService.GetCaseCommunicationFeedbackByIdAsync(communicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCaseCommunicationSurveyByIdAsync(int communicationId)
        {
            try
            {
                var result = await _caseManagementService.GetCaseCommunicationSurveyByIdAsync(communicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCustomerCaseSurveyTemplateAsync(int caseTypeId)
        {
            var result = await _caseManagementService.GetCustomerCaseSurveyTemplateAsync(caseTypeId);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FlyFoneOutboundCall(FlyFoneOutboundCallRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.FlyFoneOutboundCallAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFlyFoneCallDetailRecordsAsync()
        {
            try
            {
                var result = await _caseManagementService.GetFlyFoneCallDetailRecordsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FlyFoneEndOutboundCallAsync(FlyFoneCallDetailRecordRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.FlyFoneEndOutboundCallAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> FlyFoneFetchDetailRecordsAsync(FlyFoneFetchCallDetailRecordRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.FlyFoneFetchDetailRecordsAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CloudTalkMakeACallAsync(CloudTalkMakeACallRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.CloudTalkMakeACallAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CloudTalkGetCallAsync(CloudTalkGetCallRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.CloudTalkGetCallAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        #region Communication Review
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommunicationReviewLookupsAsync()
        {
            try
            {
                var result = await _caseManagementService.GetCommunicationReviewLookupsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateCommunicationReviewLimitAsync(CommunicationReviewLimitRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.ValidateCommunicationReviewLimitAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertCommunicationReviewEventLogAsync(CommunicationReviewEventLogRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.InsertCommunicationReviewEventLogAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> SaveCommunicationReviewAsync([FromBody] SaveCommunicationReviewRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.SaveCommunicationReviewAsync(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetCriteriaListByMeasurementId(int? measurementId)
        {
            try
            {
                var result = await _caseManagementService.GetCriteriaListByMeasurementId(measurementId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<ResponseModel> GetCommunicationHistoryByReviewIdAsync([FromBody] CommunicationReviewRequestModel request)
        {
            try
            {
                return await _messagePublisherService.GetCommunicationHistoryByReviewIdAsync(request) ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> GetCommunicationReviewHistoryListAsync([FromBody] CommunicationReviewHistoryRequestModel request)
        {
            var result = await _messagePublisherService.GetCommunicationReviewHistoryListAsync(request);

            return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCommunicationReviewEventLogAsync(int caseCommunicationId)
        {
            try
            {
                return Ok(await _caseManagementService.GetCommunicationReviewEventLogAsync(caseCommunicationId));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCommReviewPrimaryTaggingAsync(UpdateCommReviewTaggingModel request)
        {
            try
            {
                var result = await _caseManagementService.UpdateCommReviewPrimaryTaggingAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> RemoveCommReviewPrimaryTaggingAsync(RemoveCommReviewPrimaryTaggingModel request)
        {
            try
            {
                var result = await _caseManagementService.RemoveCommReviewPrimaryTaggingAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
        #endregion

        #region Samespace VoIP Integration

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SamespaceMakeACallAsync(SamespaceMakeACallRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.SamespaceMakeACallAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SamespaceGetCallAsync(SamespaceGetCallRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.SamespaceGetCallAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }
        #endregion

        #region Case and Communication
    

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCaseCommunicationAnnotationByCaseCommunicationIdAsync(long caseCommunicationId)
        {
            try
            {
                var result = await _caseManagementService.GetCaseCommunicationAnnotationByCaseCommunicationIdAsync(caseCommunicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertCaseCommunicationAnnotationAsync(CaseCommAnnotationRequestModel request)
        {
            try
            {
                var result = await _caseManagementService.UpsertCaseCommunicationAnnotationAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateCaseCommunicationAnnotationAsync(long caseCommunicationId, string contentBefore, string contentAfter)
        {
            try
            {
                var result = await _caseManagementService.ValidateCaseCommunicationAnnotationAsync( caseCommunicationId,  contentBefore,  contentAfter);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(new { message = ex.Message });
            }
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetEditCustomerServiceCaseCampainNameByUsername(string username, long brandId)
        {
            try
            {
                var result = await _caseManagementService.GetEditCustomerServiceCaseCampainNameByUsername(username, brandId);

                if (result == null)
                    return NotFound(new { message = "Campaign Name not Found." });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetChatStatisticsByCommunicationIdAsync(long communicationId)
        {
            try
            {
                var result = await _caseManagementService.GetChatStatisticsByCommunicationIdAsync(communicationId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
