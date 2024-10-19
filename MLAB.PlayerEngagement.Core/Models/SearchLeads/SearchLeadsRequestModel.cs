namespace MLAB.PlayerEngagement.Core.Models.SearchLeads
{
    public class SearchLeadsRequestModel : BaseModel
    {
        public string LeadName { get; set; }
        public string LinkedPlayerUsername { get; set; }
        public string StageIDs { get; set; }
        public string SourceId { get; set; }
        public string BrandIDs { get; set; }
        public string CurrencyIDs { get; set; }
        public string VIPLevelIDs { get; set; }
        public string CountryIDs { get; set; }
        public int PageSize { get; set; }
        public int OffsetValue { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string? LeadIds { get; set; }
    }
}