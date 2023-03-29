using ABB.Interview.API.Devices.Models;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;

namespace ABB.Interview.API.Devices.Controllers;

[ApiController]
[Route("api/devices")]
public class DeviceController : ControllerBase
{
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(ILogger<DeviceController> logger)
    {
        Guard.Against.Null(logger);

        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        await Task.Delay(1000, cancellationToken);

        return Ok(Array.Empty<DeviceListModel>());
    }
}