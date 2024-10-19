namespace MLAB.PlayerEngagement.Core.Models.Message;

public class AddMessageTypeModel
{
    public int Id { get; set; }
    public string MessageTypeName { get; set; }
    public int CodeListId { get; set; }
    public int Position { get; set; }
    public bool IsActive { get; set; }
    public string ChannelTypeId { get; set; }
    public string MessageGroupId { get; set; }
    public int CreatedBy { get; set; }
    public int UpdatedBy { get; set; }
}
