using Research.Eth.Commons;
using Research.Eth.Core.Models;

namespace Research.Eth.Core;

public interface ITransactionManager
{
    ValueTask<IResult<Estimates>> GetEstimatesInfo(GetEstimatesRequest estimatesRequest);
    
    ValueTask<IResult<SendTransactionResult>> SendTransaction(SendTransactionRequest transferRequest);
}