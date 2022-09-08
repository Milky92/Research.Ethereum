using MongoDB.Bson;

namespace Research.Eth.Persistence.Models;

[Serializable]
public class PingPong:EntityBase<Guid>
{
    public string Value { get; set; }
}