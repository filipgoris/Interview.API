using ABB.Interview.Domain;
using System.Text.Json.Serialization;

namespace ABB.Interview.API.Devices.Models;

public class DeviceListModelMax
{
    public DeviceListModelMax(KeyValuePair<Device, Power[]> device)
    {
        DeviceId = device.Key.ResourceId.ToString().Replace("-", string.Empty);
        Group = device.Key.DeviceGroup;
        Direction = device.Key.Direction.ToString();
        Power = Math.Round(device.Value.Select(p => p.Max).Max(), 4);
    }

    public string DeviceId { get; }
    public string Group { get; }
    public string Direction { get; }
    [JsonPropertyName("power.max")]
    public double Power { get; }

}
