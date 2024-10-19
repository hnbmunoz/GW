using MediatR;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Application.Mappers;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Entities;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;

namespace MLAB.PlayerEngagement.Application.Handlers;

public class CreateQueueHandler : IRequestHandler<CreateQueueCommand,QueueResponse>
{
    private readonly IQueueFactory _queueFactory;
    private readonly ILogger<CreateQueueHandler> _logger;
    public CreateQueueHandler(IQueueFactory queueFactory, ILogger<CreateQueueHandler> logger)
    {
        _queueFactory = queueFactory;
       _logger = logger;
    }
    public async Task<QueueResponse> Handle(CreateQueueCommand request, CancellationToken cancellationToken)
    {
        var queueEntitiy = QueueMapper.Mapper.Map<Queue>(request);
        if (queueEntitiy is null)
        {
            throw new ApplicationException("Issue with mapper");
        }
        var isSuccess = await _queueFactory.InsertQueueRequestAsync(queueEntitiy);
       _logger.LogInfo($"CreateQueueHandler Response Status: {isSuccess}");
        var response = new QueueResponse { result = isSuccess };
        return response;
    }
}
