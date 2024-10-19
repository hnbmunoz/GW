using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;
using MLAB.PlayerEngagement.Core.Services;
using System.Net;

namespace MLAB.PlayerEngagement.Gateway.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TicketManagementController : BaseController
    {
        private readonly IMessagePublisherService _messagePublisherService;
        private readonly ITicketManagementService _ticketManagementService;

        public TicketManagementController(IMessagePublisherService messagePublisherService, ITicketManagementService ticketManagementService)
        {
            _messagePublisherService = messagePublisherService;
            _ticketManagementService = ticketManagementService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetIcoreTransactionDataAsync([FromBody] TransactionDataRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.GetIcoreTransactionDataAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFmboTransactionDataAsync([FromBody] FmboTransactionDataRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.GetFmboTransactionDataAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMlabTransactionDataAsync(GetMlabRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.GetMlabTransactionDataAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            { 
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketTypesAsync()
        {
            try
            {
                var result = await _ticketManagementService.GetTicketTypesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketLookUpByFieldIdAsync(string fieldId)
        {
            try
            {
                var result = await _ticketManagementService.GetTicketLookUpByFieldIdAsync(fieldId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketFieldMappingByTicketTypeAsync(string ticketTypeId)
        {

            try
            {
                var result = await _ticketManagementService.GetTicketFieldMappingByTicketTypeAsync(ticketTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketCustomGroupByTicketTypeAsync(string ticketTypeId)
        {

            try
            {
                var result = await _ticketManagementService.GetTicketCustomGroupByTicketTypeAsync(ticketTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlayerByFilterAsync(string brandId, string playerId, string playerUsername)
        {

            TicketPlayerRequestModel requestModel = new TicketPlayerRequestModel();

            requestModel.BrandId = int.Parse(brandId);
            requestModel.PlayerId = playerId;
            requestModel.PlayerUsername = playerUsername;
            try
            {
                var result = await _ticketManagementService.GetPlayerByFilterAsync(requestModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> SaveTicketDetailsAsync([FromBody] SaveTicketDetailsRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.SaveTicketDetailsAsync(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketStatusHierarchyByTicketTypeAsync(TicketStatusHierarchyRequestModel request)
        {

            try
            {
                var result = await _ticketManagementService.GetTicketStatusHierarchyByTicketTypeAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketInfoByIdAsync(string ticketTypeSequenceId, string ticketTypeId)
        {
            try
            {
                var result = await _ticketManagementService.GetTicketInfoByIdAsync(ticketTypeSequenceId, ticketTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> DeleteTicketAttachmentByIdAsync([FromBody] DeleteAttachmentRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.DeleteTicketAttachmentByIdAsync(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> GetTicketCommentByTicketCommentId([FromBody] GetTicketCommentRequestModel request)
        {
            var result = await _messagePublisherService.GetTicketCommentByTicketCommentId(request);

            return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> UpsertTicketComment([FromBody] UpsertTicketCommentRequestModel request)
        {
            var result = await _messagePublisherService.UpsertTicketComment(request);

            return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> DeleteTicketCommentByTicketCommentId([FromBody] DeleteTicketCommentRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.DeleteTicketCommentByTicketCommentId(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");

            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTeamAssignmentAsync()
        {
            try
            {
                var result = await _ticketManagementService.GetTeamAssignmentAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateUnfinishedTransactionIdByTicketAsync(string transactionId, string ticketTypeId, string fieldId)
        {
            try
            {
                var result = await _ticketManagementService.ValidateUnfinishedTransactionIdByTicketAsync(transactionId, ticketTypeId, fieldId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }

        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> UpsertPopupTicketDetailsAsync([FromBody] UpsertPopupTicketDetailsRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.UpsertPopupTicketDetailsAsync(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");

            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketStatusPopupMappingAsync(long ticketTypeId)
        {
            try
            {
                var result = await _ticketManagementService.GetTicketStatusPopupMappingAsync(ticketTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAssigneesByIdsAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, long departmentId, decimal adjustmentAmount)
        {
            try
            {
                var result = await _ticketManagementService.GetAssigneesByIdsAsync(statusId, ticketTypeId, paymentMethodId, mlabPlayerId, ticketId, departmentId, adjustmentAmount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InsertTicketAttachmentAsync([FromBody] InsertTicketAttachmentRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.InsertTicketAttachmentAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAutoAssignedIdAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, string ticketCode, string statusDescription, long departmentId, decimal adjustmentAmount)
        {
            try
            {
                var result = await _ticketManagementService.GetAutoAssignedIdAsync(statusId, ticketTypeId, paymentMethodId, mlabPlayerId, ticketId, ticketCode, statusDescription, departmentId, adjustmentAmount);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketThresholdAsync([FromBody] GetTicketThresholdRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.GetTicketThresholdAsync(request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilterIDByUserId(string userId)
        {
            try
            {
                var result = await _ticketManagementService.GetFilterIDByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSavedFilterByFilterId(int filterId)
        {
            try
            {
                var result = await _ticketManagementService.GetSavedFilterByFilterId(filterId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTicketManagementLookupsAsync()
        {
            try
            {
                var result = await _ticketManagementService.GetTicketManagementLookupsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> GetSearchTicketByFilters([FromBody] SearchTicketFilterRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.GetSearchTicketByFilters(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");

            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> UpsertSearchTicketFilter([FromBody] UpsertSearchTicketFilterRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.UpsertSearchTicketFilter(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");

            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportSearchTicketByFilters([FromBody] SearchTicketFilterRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.ExportSearchTicketByFilters(request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionFieldMappingAsync(long ticketTypeId)
        {
            try
            {
                var result = await _ticketManagementService.GetTransactionFieldMappingAsync(ticketTypeId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpsertTransactionDataFromApiAsync([FromBody] UpsertTransactionDataFromApiRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.UpsertTransactionDataFromApiAsync(request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHiddenPaymentMethodTicketsAsync([FromBody] PaymentMethodHiddenTicketFieldsRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.GetHiddenPaymentMethodTicketsAsync(request);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostManualBalanceCorrection([FromBody] ManualBalanceCorrectionRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.PostManualBalanceCorrection(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostICoreHoldWithdrawal([FromBody] HoldWithdrawalRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.PostICoreHoldWithdrawal(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTransactionStatusReferenceAsync()
        {
            try
            {
                var result = await _ticketManagementService.GetTransactionStatusReferenceAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "An error occurred: " + ex.Message });
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAdjustmentBusinessTypeList()
        {
            try
            {
                var result = await _ticketManagementService.GetAdjustmentBusinessTypeList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> GetTicketHistory([FromBody] HistoryCollaboratorGridRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.GetTicketHistory(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");

            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateAddUserAsCollaborator([FromBody] ValidateAddUserAsCollaboratorRequestsModel request)
        {
            try
            {
                var result = await _ticketManagementService.ValidateAddUserAsCollaborator(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUserAsCollaborator([FromBody] AddDeleteUserAsCollaboratorRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.DeleteUserAsCollaborator(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ResponseModel> GetCollaboratorGridList([FromBody] HistoryCollaboratorGridRequestModel request)
        {
            try
            {
                var result = await _messagePublisherService.GetCollaboratorGridList(request);
                return result ? new ResponseModel() : new ResponseModel((int)HttpStatusCode.InternalServerError, "Problem encountered");

            }
            catch (Exception ex)
            {
                return new ResponseModel((int)HttpStatusCode.InternalServerError, "An error occurred: " + ex.Message);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserCollaboratorList()
        {
            try
            {
                var result = await _ticketManagementService.GetUserCollaboratorList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ValidateUserTier([FromBody] ValidateUserTierRequestModel request)
        {
            try
            {
                var result = await _ticketManagementService.ValidateUserTierAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPaymentProcessor()
        {
            try
            {
                var result = await _ticketManagementService.GetAllPaymentProcessorAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
