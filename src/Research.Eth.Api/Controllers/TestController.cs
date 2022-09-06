using MediatR;
using Microsoft.AspNetCore.Mvc;
using Research.Eth.Core.Features.PingPong;

namespace Research.Eth.Api.Controllers;

[ApiController]
[Route("[controller]/")]
public class TestController:ControllerBase
{
    private readonly IMediator _mediator;
    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("ping")]
    public async Task<IActionResult> Ping()
    {
        var res = await _mediator.Send(new PingPongQuery());
        
        return Ok(res.Data);
    }
}