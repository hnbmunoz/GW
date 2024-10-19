using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Response;

namespace MLAB.PlayerEngagement.Core.Services
{
    public interface ITicketManagementService
    {
        public Task<GetIcoreTransactionDataResponseModel> GetIcoreTransactionDataAsync(TransactionDataRequestModel request);
        public Task<TicketIntegrationResponseModel> GetFmboTransactionDataAsync(FmboTransactionDataRequestModel request);
        public Task<MlabTransactionResponseModel> GetMlabTransactionDataAsync(GetMlabRequestModel request);
        public Task<List<TicketStatusHierarchyResponseModel>> GetTicketStatusHierarchyByTicketTypeAsync(TicketStatusHierarchyRequestModel request);
        public Task<TicketInfoResponseModel> GetTicketInfoByIdAsync(string ticketId, string ticketTypeId);
        public Task<ValidateTransactionIdResponsModel> ValidateUnfinishedTransactionIdByTicketAsync(string transactionId, string ticketTypeId, string fieldId);
        public Task<long> InsertTicketAttachmentAsync(InsertTicketAttachmentRequestModel request);
        public Task<List<AssigneeResponseModel>> GetAssigneesByIdsAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, long departmentId, decimal adjustmentAmount);
        public Task<AutoAssignedIdResponseModel> GetAutoAssignedIdAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, string ticketCode, string statusDescription, long departmentId, decimal adjustmentAmount);
        #region Ticket Configuration
        public Task<List<TicketTypesResponseModel>> GetTicketTypesAsync();
        public Task<List<LookupModel>> GetTicketLookUpByFieldIdAsync(string filter);
        public Task<FieldMappingConfigurationModel> GetTicketFieldMappingByTicketTypeAsync(string ticketTypeId);
        public Task<List<TicketCustomGroupingResponseModel>> GetTicketCustomGroupByTicketTypeAsync(string ticketTypeId);
        
        public Task<TicketPlayerResponseModel> GetPlayerByFilterAsync(TicketPlayerRequestModel request);



        #endregion

        public Task<List<TeamAssignmentResponseModel>> GetTeamAssignmentAsync();
        public Task<List<TicketStatusPopupMappingResponseModel>> GetTicketStatusPopupMappingAsync(long ticketTypeId);
        public Task<int> GetFilterIDByUserId(string userId);
        public Task<SearchFilterResponseModel> GetSavedFilterByFilterId(int filterId);
        public Task<TicketManagementLookupsResponseModel> GetTicketManagementLookupsAsync();
        public Task<List<TicketThresholdResponseModel>> GetTicketThresholdAsync(GetTicketThresholdRequestModel request);
        public Task<List<SearchTicketResponseModel>> ExportSearchTicketByFilters(SearchTicketFilterRequestModel request);
        public Task<List<TransactionFieldMappingResponseModel>> GetTransactionFieldMappingAsync(long ticketTypeId);
        public Task<int> UpsertTransactionDataFromApiAsync(UpsertTransactionDataFromApiRequestModel request);
        public Task<List<PaymentMethodHiddenTicketFieldsResponseModel>> GetHiddenPaymentMethodTicketsAsync(PaymentMethodHiddenTicketFieldsRequestModel request);
        public Task<TicketIntegrationResponseModel> PostManualBalanceCorrection(ManualBalanceCorrectionRequestModel request);
        public Task<TicketIntegrationResponseModel> PostICoreHoldWithdrawal(HoldWithdrawalRequestModel request);
        public Task<List<LookupModel>> GetAdjustmentBusinessTypeList();
        public Task<List<TransactionStatusReferenceResponseModel>> GetTransactionStatusReferenceAsync();
        public Task<bool> ValidateAddUserAsCollaborator(ValidateAddUserAsCollaboratorRequestsModel request);
        public Task<bool> DeleteUserAsCollaborator(AddDeleteUserAsCollaboratorRequestModel request);
        public Task<List<LookupModel>> GetUserCollaboratorList();
        public Task<bool> ValidateUserTierAsync(ValidateUserTierRequestModel request);
        public Task<List<GetAllPaymentProcessorResponseModel>> GetAllPaymentProcessorAsync();
    }
}
