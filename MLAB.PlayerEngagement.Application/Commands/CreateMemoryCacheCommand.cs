using MediatR;
using MLAB.PlayerEngagement.Application.Responses;

namespace MLAB.PlayerEngagement.Application.Commands;

public class CreateMemoryCacheCommand : IRequest<MemoryCacheResponse>
{
    public string Id { get; set; }
    public object Data { get; set; }
}
