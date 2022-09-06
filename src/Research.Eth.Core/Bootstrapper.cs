using Common.Logging;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Research.Eth.Persistence.Mongo.Extensions;

namespace Research.Eth.Core;

public static class Bootstrapper
{
    public static IServiceCollection Bootstrap(this IServiceCollection services,IConfiguration configuration)
    => services
        .AddMongoDbContext(configuration)
        .AddMediatR(typeof(Bootstrapper));
}