namespace Research.Eth.Core.Models;

public record SendTransactionRequest
    (
        string SenderPublicKey,
        string ReceiverPublicKey,
        string TokenAddress,
        decimal Amount,
        GasInfo Gas
    );