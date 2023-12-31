﻿using ABB.Interview.Domain;
using System.Text.Json.Serialization;

namespace ABB.Interview.API.Devices.Models;

public class DeviceListModel
{
    public DeviceListModel(KeyValuePair<Device, Power[]> device)
    {
        DeviceId = device.Key.ResourceId.ToString().Replace("-", string.Empty);
        Group = device.Key.DeviceGroup;
        Direction = device.Key.Direction.ToString();
        Power = device.Value.Select(p => Math.Round(p.Max, 4)).OrderBy(max => max).ToArray();
    }

    public string DeviceId { get; }
    public string Group { get; }
    public string Direction { get; }
    [JsonPropertyName("power.max")]
    public double[] Power { get; }
}