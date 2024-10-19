using MLAB.PlayerEngagement.Core.Repositories;
using StackExchange.Redis.Extensions.Core.Abstractions;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;


namespace MLAB.PlayerEngagement.Infrastructure.Repositories;

public class CacheDbRepository : ICacheDbRepository
{
    private readonly IRedisCacheClient _redisCacheClient;
    private readonly ILogger<CacheDbRepository> _logger;

    public CacheDbRepository(IRedisCacheClient redisCacheClient, ILogger<CacheDbRepository> logger)
    {
        _redisCacheClient = redisCacheClient;
        _logger = logger;
    }

    public async Task<bool> AddItemAsync(string id, object data)
    {
        try
        {
            var redisConfiguration = _redisCacheClient.GetDbFromConfiguration();
            await redisConfiguration.AddAsync(id, data, DateTimeOffset.Now.AddHours(1));
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Error in creating data in Redis Cache Server", e);
            return false;

        }
       
    }
    public async Task<object> GetItemAsync(string id)
    {
        try
        {
            var redisConfiguration = _redisCacheClient.GetDbFromConfiguration();
            return await redisConfiguration.GetAsync<object>(id);
        }
        catch (Exception e)
        {
            //throw new ApplicationException("Error in getting  data in Redis Cache Server", e);
            _logger.LogError("Error in getting  data in Redis Cache Server", e);
            return null;
        }
    }
}
