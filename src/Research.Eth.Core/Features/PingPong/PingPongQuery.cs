using MediatR;
using Research.Eth.Commons.Models;

namespace Research.Eth.Core.Features.PingPong;

public class PingPongQuery:IRequest<Result<string>>
{
    
}