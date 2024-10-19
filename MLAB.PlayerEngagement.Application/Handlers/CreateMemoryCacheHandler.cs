using MediatR;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Application.Mappers;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Entities;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;

namespace MLAB.PlayerEngagement.Application.Handlers;

public class CreateMemoryCacheHandler : IRequestHandler<CreateMemoryCacheCommand, MemoryCacheResponse>
{
    private readonly ICacheDbRepository _cacheFactory;
    private readonly ILogger<CreateMemoryCacheHandler> _logger;
    public CreateMemoryCacheHandler(ICacheDbRepository cacheFactory, ILogger<CreateMemoryCacheHandler> logger)
    {
        _cacheFactory = cacheFactory;
        _logger = logger;
    }
    public async Task<MemoryCacheResponse> Handle(CreateMemoryCacheCommand item, CancellationToken cancellationToken)
    {
        var itemEntitiy = CacheMapper.Mapper.Map<Cache>(item);

        if (itemEntitiy is null)
        {
            throw new ApplicationException("Issue with mapper");
        }
        var isSuccess = await _cacheFactory.AddItemAsync(itemEntitiy.Id, itemEntitiy.Data);
        _logger.LogInfo($"CreateMemoryCacheHandler Response Status: {isSuccess}");
        var response = new MemoryCacheResponse { result = isSuccess };
        return response;
    }



}
