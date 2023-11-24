using ABB.Interview.API.DeviceGroups.Models;
using ABB.Interview.Contracts;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;

namespace ABB.Interview.API.DeviceGroups.Controllers;

[ApiController]
[Route("api/groups")]
public class DeviceGroupController : APIBaseController
{
    private readonly ILogger<DeviceGroupController> _logger;

    public DeviceGroupController(ILogger<DeviceGroupController> logger, IMeasurementReader reader, IConfiguration config) : base(reader, config)
    {
        Guard.Against.Null(logger);
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            var measures = await GetMeasures(cancellationToken);
            if (measures == null) return Problem("No measurements found.");

            var test = measures.GroupBy(kvp => new
            {
                kvp.Key.DeviceGroup,
                kvp.Key.Direction
            })
                .Select(g => new DeviceGroupListModel()
                {
                    Group = g.Key.DeviceGroup,
                    Direction = g.Key.Direction.ToString(),
                    Power = Math.Round(g.Sum(x => x.Value.Sum(p => p.Avg)), 4)
                })
                .OrderBy(x => x.Group)
                .ThenBy(x => x.Direction)
                ;

            return Ok(test);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in {Action}: {Exception}", nameof(Get), ex);
            return Problem("Something went wrong on the server");
        }
    }
}