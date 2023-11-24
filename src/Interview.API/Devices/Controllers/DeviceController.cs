using ABB.Interview.API.Devices.Models;
using ABB.Interview.Contracts;
using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;


namespace ABB.Interview.API.Devices.Controllers;

[ApiController]
[Route("api/devices")]
public class DeviceController : APIBaseController
{
    private readonly ILogger<DeviceController> _logger;

    public DeviceController(ILogger<DeviceController> logger, IMeasurementReader reader, IConfiguration config) : base(reader, config)
    {
        Guard.Against.Null(logger);
        _logger = logger;
    }

    /// <summary>
    ///     Devices ordered ascending by Group and Direction, with ascending list of power.max/device
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="500">Something went wrong on the server</response>
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        try
        {
            var measures = await GetMeasures(cancellationToken);
            if (measures == null) return Problem("No measurements found.");

            var result = measures.Select(m => new DeviceListModel(m)).ToArray()
                .OrderBy(d => d.Group)
                .ThenBy(d => d.Direction);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in {Action}: {Exception}", nameof(Get), ex);
            return Problem("Something went wrong on the server");
        }
    }

    /// <summary>
    ///     Devices ordered ascending by Group, Direction, and Max(power.max)
    /// </summary>
    /// <response code="200">Success</response>
    /// <response code="500">Something went wrong on the server</response>
    [HttpGet]
    [Route("max")]
    public async Task<IActionResult> GetMax(CancellationToken cancellationToken)
    {
        try
        {
            var measures = await GetMeasures(cancellationToken);
            if (measures == null) return NotFound();

            var result = measures.Select(m => new DeviceListModelMax(m)).ToArray()
                .OrderBy(d => d.Group)
                .ThenBy(d => d.Direction)
                .ThenBy(d => d.Power);

            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception in {Action}: {Exception}", nameof(GetMax), ex);
            return Problem("Something went wrong on the server");
        }
    }
}