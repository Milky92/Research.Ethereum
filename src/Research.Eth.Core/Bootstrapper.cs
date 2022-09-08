using System.Reflection;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Research.Eth.DataAccess;

namespace Research.Eth.Core;

public static class Bootstrapper
{
    public static IServiceCollection Bootstrap(this IServiceCollection services, IConfiguration configuration)
    {
        var s = services;
        s.AddMediatR(typeof(Bootstrapper));
        
        services
            .AddAppDbContext(configuration);

        return services;
    }
}