namespace MLAB.PlayerEngagement.Core.Response;

public class CountryListResponse
{
    public long CountryId { get; set; }
    public string CountryCode { get; set; }
    public string CountryName { get; set; }
    public int Status { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public string UpdatedBy { get; set; }
    public string UpdatedDate { get; set; }
}
