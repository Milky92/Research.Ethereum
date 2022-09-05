using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Research.Eth.Persistence.Mongo.Internals;

public class MongoDb : IMongoDb
{
    private bool _disposed = false;
    private readonly DatabaseSettings _options;
    private readonly IMongoClient _mongoClient;

    public MongoDb(IOptions<DatabaseSettings> options)
    {
        _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        
        _mongoClient = new MongoClient(BuildSettings(_options));
    }

    public async ValueTask<IMongoDatabase> Initialize(MongoDbContext context,CancellationToken token)
    {
        //oh that simple.. 
        return _mongoClient.GetDatabase(_options.DatabaseOptions.Name);
    }

    private MongoClientSettings BuildSettings(DatabaseSettings options)
    {
        var mongoClientSettings = new MongoClientSettings();

        if (!string.IsNullOrEmpty(options.ConnectionString))
            return MongoClientSettings.FromConnectionString(options.ConnectionString);

        if (options.Server is null)
            throw new InvalidOperationException("Please set server configuration options.");

        if (options.DatabaseOptions is null)
            throw new InvalidOperationException("Please set database configuration options.");
        
        if (string.IsNullOrEmpty(options.DatabaseOptions.Name))
            throw new InvalidOperationException("Please set database name.");

        mongoClientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
        
        mongoClientSettings.Server = options.Server?.Address.Port is not null
            ? new MongoServerAddress(options.Server?.Address.Host, options.Server?.Address.Port ?? default)
            : new MongoServerAddress(options.Server?.Address.Host);

        mongoClientSettings.UseTls = options.Server.UseTls;
        mongoClientSettings.RetryReads = options.Server.RetryReads;
        
        return mongoClientSettings;
    }
}