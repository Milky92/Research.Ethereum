using Research.Eth.Core.Models;
using Nethereum.Web3;
using System.Numerics;
using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.Signer;
using Research.Eth.Commons.Models;

namespace Research.Eth.Core.Internals;

internal class AccountManager : IAccountManager
{
    public AccountManager()
    {
    }

    public ValueTask<Result<Account>> CreateNew(CancellationToken token)
    {
        var ecKey = EthECKey.GenerateKey();
        return ValueTask.FromResult(Result<Account>.Ok(new Account(ecKey.GetPublicAddress(), ecKey.GetPublicAddress())));
    }

    public ValueTask<Result<AccountStatements>> GetAccountStatements(string publicKey, CancellationToken token)
    {
        return ValueTask.FromResult(Result<AccountStatements>.NotImplemented());
    }
}