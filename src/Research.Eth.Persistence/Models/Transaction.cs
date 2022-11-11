using System.Transactions;
using MongoDB.Bson;

namespace Research.Eth.Persistence.Models;

public class Transaction:EntityBase<ObjectId>
{
    public string Hash { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string TokenAddress { get; set; }
    public long Value { get; set; }
    public long Fee { get; set; }
    public string TraceTypeAddress { get; set; }
    public int Nonce { get; set; }
    public int Position { get; set; }

    /// <summary>
    /// Any user data
    /// </summary>
    public string Payload { get; set; }

    public Estimate Estimate { get; set; }
    public TransactionStatus Status { get; set; }
    public Block Block { get; set; }
    public Other Other { get; set; }

    public ICollection<Transaction> TokenTrx { get; set; }

    public ICollection<Transaction> InternalTrx { get; set; }

    public Dictionary<string, string> Metadata { get; set; }
}

public class Other
{
    public int Nonce { get; set; }
    public int Position { get; set; }
}