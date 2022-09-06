using MediatR;
using Research.Eth.Commons.Models;

namespace Research.Eth.Core.Features.Account;

public class GetAccountQuery:IRequest<Result<Models.Account>>
{
    public string PublicKey { get; set; }
}