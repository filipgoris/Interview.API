using ABB.Interview.Contracts;
using ABB.Interview.Domain;
using ABB.Interview.Data.Dto;
using AutoMapper;
using System.Text.Json;

namespace ABB.Interview.Data
{
    public class MeasurementReader : IMeasurementReader
    {
        private readonly IMapper _mapper;

        public MeasurementReader(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<Dictionary<Device, Power[]>?> ReadAsync(string measurementFilePath, CancellationToken cancellationToken)
        {
            if (!File.Exists(measurementFilePath))
                throw new ApplicationException($"Measurement file not found @ {measurementFilePath}");

            using FileStream measurementStream = File.OpenRead(measurementFilePath);
            List<DeviceReadingDto>? measurementReadings = await JsonSerializer.DeserializeAsync<List<DeviceReadingDto>>(measurementStream, options: null, cancellationToken);

            if (measurementReadings == null || !measurementReadings.Any())
                throw new ApplicationException($"Invalid measurement file: {measurementFilePath}");

            Dictionary<Device, Power[]> measurements = measurementReadings.ToDictionary(x => _mapper.Map<Device>(x), x => x.Power.ToArray());

            return measurements;
        }
    }
}