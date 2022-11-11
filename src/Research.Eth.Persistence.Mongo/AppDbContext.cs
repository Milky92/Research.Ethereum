using Microsoft.Extensions.Options;
using Research.Eth.Persistence.Extensions.Wrappers;
using Research.Eth.Persistence.Models;

namespace Research.Eth.Persistence.Mongo;

public class AppDbContext:MongoDbContext
{
    public AppDbContext(IServiceProvider serviceProvider, IOptions<DatabaseSettings> options) : base(serviceProvider, options)
    {
    }
    public NoSqlCollection<PingPong> PingPong { get; set; }
}