namespace MLAB.PlayerEngagement.Core.Models.PlayerConfiguration;

public class LanguageModel
{
    public int Id { get; set; }
    public string LanguageName { get; set; }
    public string LanguageCode { get; set; }
    public bool IsComplete { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreateDate { get; set; }
    public string UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
