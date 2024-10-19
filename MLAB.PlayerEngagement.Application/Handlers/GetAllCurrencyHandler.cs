using MediatR;
using MLAB.PlayerEngagement.Application.Mappers;
using MLAB.PlayerEngagement.Application.Queries;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Repositories;
using MLAB.PlayerEngagement.Core.Response;

namespace MLAB.PlayerEngagement.Application.Handlers;

public class GetAllCurrencyHandler : IRequestHandler<GetAllCurrencyQuery, List<AllCurrencyResponse>>
{
    private readonly ISystemFactory _systemFactory;
    private readonly ILogger<GetAllCurrencyHandler> _logger;
    public GetAllCurrencyHandler(ISystemFactory systemFactory, ILogger<GetAllCurrencyHandler> logger)
    {
        _systemFactory = systemFactory;
        _logger = logger;
    }
    public async Task<List<AllCurrencyResponse>> Handle(GetAllCurrencyQuery request, CancellationToken cancellationToken)
    {
        var result = await _systemFactory.GetAllCurrencyAsync(request.UserId);
        var response = CurrencyMapper.Mapper.Map<List<AllCurrencyResponse>>(result);
        return response;
    }
}
