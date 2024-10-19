namespace MLAB.PlayerEngagement.Core.Models;

public class CodeListModel: BaseModel
{
    public int Id { get; set; }
    public int? Position { get; set; }
    public string CodeListName { get; set; }
    public string CodeListTypeName { get; set; }
    public string ParentCodeListName { get; set; }
    public int CodeListTypeId { get; set; }
    public int? ParentId { get; set; }
    public bool IsActive { get; set; }
    public int? FieldTypeId { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
    //public List<TopicModel> Topics { get; set; }
}
