using MongoDB.Bson.Serialization.Attributes;

namespace Research.Eth.Persistence.Models;

public abstract class EntityBase<TK>
{
    [BsonId]
    public TK Id { get; set; }
    public DateTime Created { get; } = DateTime.UtcNow;
    public DateTime Updated { get; set; }
}