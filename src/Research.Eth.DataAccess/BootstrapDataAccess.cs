using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Research.Eth.Persistence.Mongo.Extensions;

namespace Research.Eth.DataAccess;

public static class BootstrapDataAccess
{
    public static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        => services
            .AddMongoDbContext(configuration)
            .AddTransient<AppDbContext>();
}