using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Research.Eth.Persistence.Extensions.Wrappers;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class NoSqlCollection<T>
{
    private readonly IMongoDatabase _mongoDatabase;

    private readonly IClientSessionHandle _session;

    //temporary concrete type instead abstract type on driver
    public NoSqlCollection(IMongoDatabase mongoDatabase, IClientSessionHandle session)
    {
        _mongoDatabase = mongoDatabase;
        _session = session;
    }

    private IMongoCollection<T> Collection => _mongoDatabase.GetCollection<T>(nameof(T));

    private IMongoQueryable<T> Query => Collection.AsQueryable();

    public async ValueTask<T> FirstOrDefault(Expression<Func<T, bool>> predicate, CancellationToken token)
        => await Query.FirstOrDefaultAsync(predicate, token);

    public async ValueTask<T> SingleOrDefault(Expression<Func<T, bool>> predicate, CancellationToken token)
        => await Query.SingleOrDefaultAsync(predicate, token);

    public async ValueTask<T> Min(Expression<Func<T, T>> selector, CancellationToken token)
        => await Query.MinAsync(selector, token);
    
    public async ValueTask<T> Max(Expression<Func<T, T>> selector, CancellationToken token)
        => await Query.MaxAsync(selector, token);

    public IQueryable<T> Where(Expression<Func<T, bool>> expression, CancellationToken token)
        => Query.Where(expression).AsQueryable();

    public async ValueTask Add(T entity, CancellationToken token) =>
        await Collection.InsertOneAsync(session: _session, document: entity, cancellationToken: token);

    public async ValueTask Remove(Expression<Func<T, bool>> predicate, CancellationToken token)
        => await Collection.DeleteOneAsync(predicate, token);

    public async ValueTask Update(Expression<Func<T, bool>> predicate, T entity, CancellationToken token)
        => await Collection.UpdateOneAsync(filter: predicate,
            update: new BsonDocumentUpdateDefinition<T>(entity.ToBsonDocument()), cancellationToken: token);
}