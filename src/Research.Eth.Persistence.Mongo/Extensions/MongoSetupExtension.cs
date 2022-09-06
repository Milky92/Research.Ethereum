using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Research.Eth.Persistence.Mongo.Internals;

namespace Research.Eth.Persistence.Mongo.Extensions;

public static class MongoSetupExtension
{
    private const string DefaultSection = "MongoDb";

    public static IServiceCollection AddMongoDbContext(this IServiceCollection container,
        IConfiguration configuration,
        string section = DefaultSection)
    {
        return container
            .Configure<DatabaseSettings>(opts => configuration.GetSection(section).Bind(opts))
            .AddSingleton<IMongoDb, MongoDb>();
    }
}