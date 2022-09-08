using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Research.Eth.Persistence.Extensions.Wrappers;

namespace Research.Eth.Persistence.Mongo;

public class MongoDbContext
{
    private readonly IMongoDb _mongodb;
    private readonly IClientSessionHandle _session;

    public MongoDbContext(IServiceProvider serviceProvider, IOptions<DatabaseSettings> options)
    {
        _mongodb = serviceProvider.GetRequiredService<IMongoDb>() ??
                   throw new ArgumentException("Could not resolve IMongoDb service.");

        _session = _mongodb.Initialize(this).Result;

        InitCollections();
    }


    private void InitCollections()
    {
        var collections = GetType().GetProperties()
            .Where(p => 
                p.PropertyType.IsGenericType &&
                p.PropertyType.GetGenericTypeDefinition() == typeof(NoSqlCollection<>)).ToList();

        collections.ForEach(
            p => p.SetValue(this, Activator.CreateInstance(p.PropertyType, _mongodb.Database, _session)));
    }
}