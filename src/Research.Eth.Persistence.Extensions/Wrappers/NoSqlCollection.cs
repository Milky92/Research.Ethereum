using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Research.Eth.Persistence.Models;

namespace Research.Eth.Persistence.Extensions.Wrappers;

/// <summary>
/// Implementation concrete repository for datasource provider. 
/// </summary>
/// <typeparam name="T"></typeparam>
public class NoSqlCollection<TEntity>
{
    private readonly IMongoDatabase _mongoDatabase;

    private readonly IClientSessionHandle _session;

    //temporary concrete type instead abstract type on driver
    public NoSqlCollection(IMongoDatabase mongoDatabase, IClientSessionHandle session)
    {
        _mongoDatabase = mongoDatabase;
        _session = session;
        var t = nameof(TEntity);
        
    }

    private IMongoCollection<TEntity> Collection => _mongoDatabase.GetCollection<TEntity>(typeof(TEntity).Name);

    private IMongoQueryable<TEntity> Query => Collection.AsQueryable();

    public async ValueTask<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
        => await Query.FirstOrDefaultAsync(predicate, token);

    public async ValueTask<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
        => await Query.SingleOrDefaultAsync(predicate, token);

    public async ValueTask<TEntity> Min(Expression<Func<TEntity, TEntity>> selector, CancellationToken token)
        => await Query.MinAsync(selector, token);

    public async ValueTask<TEntity> Max(Expression<Func<TEntity, TEntity>> selector, CancellationToken token)
        => await Query.MaxAsync(selector, token);

    public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> expression, CancellationToken token)
        => Query.Where(expression).AsQueryable();

    public async ValueTask Add(TEntity entity, CancellationToken token) =>
        await Collection.InsertOneAsync(document: entity, cancellationToken: token);

    public async ValueTask Remove(Expression<Func<TEntity, bool>> predicate, CancellationToken token)
        => await Collection.DeleteOneAsync(predicate, token);

    public async ValueTask Update(Expression<Func<TEntity, bool>> predicate, TEntity entity, CancellationToken token)
        => await Collection.ReplaceOneAsync(predicate, entity, cancellationToken: token);


    #region support

    private void RegisterMappingTypes<TEntity>()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
            BsonClassMap.RegisterClassMap<TEntity>();
    }

    #endregion
}