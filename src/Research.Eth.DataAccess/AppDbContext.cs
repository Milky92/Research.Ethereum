using Microsoft.Extensions.Options;
using Research.Eth.Persistence.Extensions.Wrappers;
using Research.Eth.Persistence.Models;
using Research.Eth.Persistence.Mongo;

namespace Research.Eth.DataAccess;

public class AppDbContext:MongoDbContext
{
    public AppDbContext(IServiceProvider serviceProvider, IOptions<DatabaseSettings> options) 
        : base(serviceProvider, options) { }
    
    public NoSqlCollection<PingPong> PingPong { get; set; }
}