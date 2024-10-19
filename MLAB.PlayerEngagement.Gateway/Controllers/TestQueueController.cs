using MediatR;
using Microsoft.AspNetCore.Mvc;
using MLAB.PlayerEngagement.Application.Commands;
using MLAB.PlayerEngagement.Application.Responses;
using MLAB.PlayerEngagement.Core.Entities;
using MLAB.PlayerEngagement.Core.Logging;
using MLAB.PlayerEngagement.Core.Logging.Extensions;

namespace MLAB.PlayerEngagement.Gateway.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class TestQueueController : ControllerBase
{

    private readonly IMediator _mediator;
    //private readonly ILogger<TestQueueController> _logger;
    public TestQueueController(IMediator mediator, IConfiguration configuration)
    {
        _mediator = mediator;
        Configuration = configuration;
    }
    public IConfiguration Configuration { get; }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<QueueResponse>> CreateQueue([FromBody] CreateQueueCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MemoryCacheResponse>> CreateMemoryCache([FromBody] CreateMemoryCacheCommand command)
    {
        var testData = new TempData { input = "test" };
        command.Data = testData;
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ExchangeResponse>> CreateQueuePublish([FromBody] CreateQueuePublishCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }



    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetFrontEndUrl()
    {
        //_logger.LogInfo("GetFrontEndUrl");
        var result = Configuration.GetConnectionString("FrontEndUrl");
        await Task.CompletedTask;
        return Ok(result);
    }
}
