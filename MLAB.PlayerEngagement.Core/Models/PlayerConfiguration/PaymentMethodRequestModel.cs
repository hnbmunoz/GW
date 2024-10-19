namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration
{
    public  class PaymentMethodRequestModel: BaseModel
    {
        public int? PaymentMethodId { get; set; }
        public int? PaymentMethodIcoreId { get; set; }
        public string PaymentMethodName { get; set; }
        public bool? PaymentMethodStatus { get; set; }
        public int? PaymentMethodVerifier { get; set; }
        public string PaymentMethodMessageTypeIds { get; set; }
        public string PaymentMethodProviderId { get; set; }
        public int? PageSize { get; set; }
        public int? OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
    }
}
