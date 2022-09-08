using MediatR;
using Microsoft.Extensions.Logging;
using Research.Eth.Commons.Models;
using Research.Eth.DataAccess;

namespace Research.Eth.Core.Features.PingPong;

public class PingPongHandler : IRequestHandler<PingPongQuery, Result<string>>
{
    private static bool ping = true;
    private readonly ILogger<PingPongHandler> _logger;
    private readonly AppDbContext _dbContext;

    public PingPongHandler(ILogger<PingPongHandler> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task<Result<string>> Handle(PingPongQuery request, CancellationToken cancellationToken)
    {
        if (ping)
            ping = false;
        else
            ping = true;

        _logger.LogInformation("Try set and get value from data source.");

        try
        {
            var pingPong = ping ? "pong" : "ping";

            var entity = await _dbContext.PingPong.FirstOrDefault(p => p.Value == pingPong, cancellationToken);

            if (entity is null)
                await _dbContext.PingPong.Add(new Persistence.Models.PingPong { Value = pingPong }, cancellationToken);
            else
                await _dbContext.PingPong.Update(
                    p => p.Id == entity.Id, new Persistence.Models.PingPong()
                    {
                        Id = entity.Id,
                        Value = pingPong
                    }, cancellationToken);

            entity = await _dbContext.PingPong.FirstOrDefault(p => p.Value == pingPong, cancellationToken);
            
            return Result<string>.Ok(entity?.Value!);
        }
        catch (Exception e)
        {
            _logger.LogError(e,"Could not get entity from data source.");
            return Result<string>.InternalError("Could not get entity from data source.");
        }
        
       
    }
}