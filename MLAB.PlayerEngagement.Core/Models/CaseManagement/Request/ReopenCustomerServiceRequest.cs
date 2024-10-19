namespace MLAB.PlayerEngagement.Core.Models.CaseManagement.Request
{
    public class ChangeStatusCustomerServiceRequest : BaseModel
    {
        public string CaseInformationIds { get; set; }
        public long CaseStatusId { get; set; }
    }
}
