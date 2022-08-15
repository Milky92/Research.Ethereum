using Research.Eth.Commons.Models;
using Research.Eth.Core.Models;

namespace Research.Eth.Core;

public interface IAccountManager
{
    ValueTask<Result<Account>> CreateNew(CancellationToken token);

    ValueTask<Result<AccountStatements>> GetAccountStatements(string publicKey, CancellationToken token);
}