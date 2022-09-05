using MongoDB.Driver;

namespace Research.Eth.Persistence.Mongo;

public interface IMongoDb
{
    ValueTask<IMongoDatabase> Initialize(MongoDbContext context,CancellationToken token);
}