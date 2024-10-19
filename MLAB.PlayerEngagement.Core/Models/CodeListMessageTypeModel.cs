using MLAB.PlayerEngagement.Core.Models.Message;

namespace MLAB.PlayerEngagement.Core.Models;

public class CodeListMessageTypeModel : BaseModel
{
    public long CodeListId { get; set; }
    public bool IsActive { get; set; }
    public List<AddMessageTypeModel> MessageTypes { get; set; }
}
