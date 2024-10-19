using MLAB.PlayerEngagement.Core.Models;
using MLAB.PlayerEngagement.Core.Models.Response;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Request;
using MLAB.PlayerEngagement.Core.Models.TicketManagement.Response;

namespace MLAB.PlayerEngagement.Core.Repositories
{
    public interface ITicketManagementFactory
    {
        Task<List<TicketTypesResponseModel>> GetTicketTypesAsync();      
        Task<List<LookupModel>> GetTicketLookUpByFieldIdAsync(string filter);        
        Task<FieldMappingConfigurationModel> GetTicketFieldMappingByTicketTypeAsync(string ticketTypeId);
        Task<List<TicketCustomGroupingResponseModel>> GetTicketCustomGroupByTicketTypeAsync(string ticketTypeId);
        Task<TicketPlayerResponseModel> GetPlayerByFilterAsync(TicketPlayerRequestModel request);
        Task<MlabTransactionResponseModel> GetMlabTransactionDataAsync(GetMlabRequestModel request);
        Task<List<TicketStatusHierarchyResponseModel>>GetTicketStatusHierarchyByTicketTypeAsync(TicketStatusHierarchyRequestModel request);
        Task<TicketInfoResponseModel> GetTicketInfoByIdAsync(string ticketTypeSequenceId, string ticketTypeId);
        Task<ValidateTransactionIdResponsModel> ValidateUnfinishedTransactionIdByTicketAsync(string transactionId, string ticketTypeId, string fieldId);
        Task<long> InsertTicketAttachmentAsync(InsertTicketAttachmentRequestModel request);
        Task<List<TeamAssignmentResponseModel>> GetTeamAssignmentAsync();
        Task<List<AssigneeResponseModel>> GetAssigneesByIdsAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, long departmentId, decimal adjustmentAmount);
        Task<AutoAssignedIdResponseModel> GetAutoAssignedIdAsync(int statusId, int ticketTypeId, int paymentMethodId, long mlabPlayerId, int ticketId, long departmentId, decimal adjustmentAmount);
	    Task<List<TicketStatusPopupMappingResponseModel>> GetTicketStatusPopupMappingAsync(long ticketTypeId);
        Task<int> GetFilterIDByUserId(string userId);
        Task<SearchFilterResponseModel> GetSavedFilterByFilterId(int filterId);
        Task<TicketManagementLookupsResponseModel> GetTicketManagementLookupsAsync();
        Task<List<TicketThresholdResponseModel>> GetTicketThresholdAsync(GetTicketThresholdRequestModel request);
        Task<List<SearchTicketResponseModel>> ExportSearchTicketByFilters(SearchTicketFilterRequestModel request);
        Task<List<TransactionFieldMappingResponseModel>> GetTransactionFieldMappingAsync(long ticketTypeId);
        Task<int> UpsertTransactionDataFromApiAsync(UpsertTransactionDataFromApiRequestModel request);
        Task<List<PaymentMethodHiddenTicketFieldsResponseModel>> GetHiddenPaymentMethodTicketsAsync(PaymentMethodHiddenTicketFieldsRequestModel request);
        Task<List<LookupModel>> GetAdjustmentBusinessTypeList();
        Task<List<TransactionStatusReferenceResponseModel>> GetTransactionStatusReferenceAsync();
        Task<bool> ValidateAddUserAsCollaborator(ValidateAddUserAsCollaboratorRequestsModel request);
        Task<bool> DeleteUserAsCollaborator(AddDeleteUserAsCollaboratorRequestModel request);
        Task<List<LookupModel>> GetUserCollaboratorList();
        Task<TicketEmailDetails> GetTicketEmailDetails(int? ticketId, int ticketTypeId, int? ticketTypeSequenceId);
        Task<bool> ValidateUserTierAsync(ValidateUserTierRequestModel request);
        Task<List<GetAllPaymentProcessorResponseModel>> GetAllPaymentProcessorAsync();
    }
}
