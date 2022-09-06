using MediatR;
using Research.Eth.Commons.Models;
using Microsoft.Extensions.Logging;
namespace Research.Eth.Core.Features.Account;

public class GetAccountQueryHandler:IRequestHandler<GetAccountQuery,Result<Models.Account>>
{
    private readonly ILogger<GetAccountQueryHandler> _logger;
    public GetAccountQueryHandler(ILogger<GetAccountQueryHandler> logger)
    {
        _logger = logger;
    }
    
    public Task<Result<Models.Account>> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}