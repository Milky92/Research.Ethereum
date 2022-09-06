using MongoDB.Driver;

namespace Research.Eth.Persistence.Mongo;

public interface IMongoDb
{
    IMongoDatabase Database { get; }
    
    /// <summary>
    /// Initial data source and collections from data context.
    /// </summary>
    /// <param name="context">Data context</param>
    /// <param name="token"></param>
    /// <returns></returns>
    ValueTask<IClientSessionHandle> Initialize(MongoDbContext context, CancellationToken token = default);

    /// <summary>
    /// Start new transaction for db.
    /// </summary>
    void StartTransaction();

    /// <summary>
    /// Commit started transaction.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ValueTask CommitTransactionASync(CancellationToken token);

    /// <summary>
    /// Aborted transaction.
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    ValueTask RollbackTransactionAsync(CancellationToken token);
}