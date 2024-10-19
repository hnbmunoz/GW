using MediatR;
using MLAB.PlayerEngagement.Core.Response;

namespace MLAB.PlayerEngagement.Application.Queries;

public class GetAllCurrencyQuery : IRequest<List<AllCurrencyResponse>>
{
    public long? UserId { get; set; }
}
