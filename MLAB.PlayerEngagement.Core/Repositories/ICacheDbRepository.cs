namespace MLAB.PlayerEngagement.Core.Repositories;

public interface ICacheDbRepository
{
    Task<bool> AddItemAsync(string id, object data);
    Task<object> GetItemAsync(string id);
}
