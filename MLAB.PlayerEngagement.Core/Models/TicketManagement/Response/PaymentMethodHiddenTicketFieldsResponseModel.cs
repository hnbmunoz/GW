namespace MLAB.PlayerEngagement.Core.Models.TicketManagement.Response
{
    public class PaymentMethodHiddenTicketFieldsResponseModel
    {
        public long FieldId { get; set; }
        public long FieldMappingId { get; set; }
        public bool IsOptional { get; set; }
        public string FieldName { get; set; }
    }
}
