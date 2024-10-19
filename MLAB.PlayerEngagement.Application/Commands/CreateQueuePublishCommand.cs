using MediatR;
using MLAB.PlayerEngagement.Application.Responses;

namespace MLAB.PlayerEngagement.Application.Commands;

public class CreateQueuePublishCommand : IRequest<ExchangeResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Action { get; set; }
    public Guid CacheId { get; set; }
    public string ExchangeUri { get; set; }
    public string CallbackUri { get; set; }
    public string QueueStatus { get; set; }
    public string UserId { get; set; }
    public string Remarks { get; set; }

}
