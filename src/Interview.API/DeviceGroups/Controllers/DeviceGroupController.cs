using ABB.Interview.API.DeviceGroups.Models;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;

namespace ABB.Interview.API.DeviceGroups.Controllers;

[ApiController]
[Route("api/groups")]
public class DeviceGroupController : ControllerBase
{
    private readonly ILogger<DeviceGroupController> _logger;

    public DeviceGroupController(ILogger<DeviceGroupController> logger)
    {
        Guard.Against.Null(logger);

        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        return Ok(Array.Empty<DeviceGroupListModel>());
    }
}