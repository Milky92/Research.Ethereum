using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Research.Eth.Persistence.Extensions.Wrappers;

namespace Research.Eth.Persistence.Mongo.Internals;

public class MongoDb : IMongoDb
{
    private readonly IMongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    private IClientSessionHandle _session;

    public MongoDb(IOptions<DatabaseSettings> options)
    {
         _ = options.Value ?? throw new ArgumentNullException(nameof(options));

        _mongoClient = new MongoClient(BuildSettings(options.Value));

        _database = _mongoClient.GetDatabase(options.Value.DatabaseOptions.Name);
    }

    public IMongoDatabase Database => _database;
    
    public async ValueTask<IClientSessionHandle> Initialize(MongoDbContext context, CancellationToken token)
    {
        var sets = InitDbCollections(context).ToList();
        
        _session = await _mongoClient.StartSessionAsync(cancellationToken: token);
        
        foreach (var (type, name) in sets)
        {
            if (type != null)
                await _database.CreateCollectionAsync(!string.IsNullOrEmpty(name) ? name : type.Name,
                    cancellationToken: token)!;
        }

        return _session;
    }

    public void StartTransaction()
    {
        _session.StartTransaction();
    }

    public async ValueTask CommitTransactionASync(CancellationToken token)
        => await _session.CommitTransactionAsync(token);
    
    public async ValueTask RollbackTransactionAsync(CancellationToken token)
    => await _session.AbortTransactionAsync(token);

    private IEnumerable<(Type?, string)> InitDbCollections(MongoDbContext context)
    {
        var collections = context.GetType().GetProperties()
            .Where(p => p.PropertyType.IsGenericType &&
                        p.PropertyType.GetGenericTypeDefinition() == typeof(NoSqlCollection<>));

        foreach (var prop in collections)
        {
            yield return (prop.PropertyType.GenericTypeArguments.FirstOrDefault(), prop.Name);
        }
    }

    private MongoClientSettings BuildSettings(DatabaseSettings options)
    {
        var mongoClientSettings = new MongoClientSettings();

        if (!string.IsNullOrEmpty(options.ConnectionString))
            return MongoClientSettings.FromConnectionString(options.ConnectionString);

        if (options.Server is null)
            throw new InvalidOperationException("Please set server configuration options.");

        if (options.Server.Address is null)
            throw new InvalidOperationException("Please set server address configuration options.");

        if (options.DatabaseOptions is null)
            throw new InvalidOperationException("Please set database configuration options.");

        if (string.IsNullOrEmpty(options.DatabaseOptions.Name))
            throw new InvalidOperationException("Please set database name.");

        mongoClientSettings.ServerApi = new ServerApi(ServerApiVersion.V1);
        mongoClientSettings.LinqProvider = LinqProvider.V3;
        
        mongoClientSettings.Server = options.Server.Address.Port is not null
            ? new MongoServerAddress(options.Server.Address.Host, options.Server.Address.Port ?? default)
            : new MongoServerAddress(options.Server.Address.Host);

        mongoClientSettings.UseTls = options.Server.UseTls;
        mongoClientSettings.RetryReads = options.Server.RetryReads;

        return mongoClientSettings;
    }
}