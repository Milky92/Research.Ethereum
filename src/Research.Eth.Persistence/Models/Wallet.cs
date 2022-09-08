using Research.Eth.Commons.Enums;

namespace Research.Eth.Persistence.Models;

public class Wallet
{
    public ProviderInfo Provider { get; set; }
    public WalletType Type { get; set; }
    public string PublicKey { get; set; }
    public string PrivateKey { get; set; }
}