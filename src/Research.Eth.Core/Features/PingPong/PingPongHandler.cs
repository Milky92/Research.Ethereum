using MediatR;
using Research.Eth.Commons.Models;

namespace Research.Eth.Core.Features.PingPong;

public class PingPongHandler:IRequestHandler<PingPongQuery,Result<string>>
{
    private static bool ping = true;
    public async Task<Result<string>> Handle(PingPongQuery request, CancellationToken cancellationToken)
    {
        if (ping)
            ping = false;
        else
            ping = true;
        
        return Result<string>.Ok(ping ? "pong": "ping");
    }
}