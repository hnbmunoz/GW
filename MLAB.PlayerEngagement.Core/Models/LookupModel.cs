namespace MLAB.PlayerEngagement.Core.Models;

public class LookupModel
{
    public int Value { get; set; }
    public string Label { get; set; }

    public static implicit operator List<object>(LookupModel v)
    {
        throw new NotImplementedException();
    }
}
