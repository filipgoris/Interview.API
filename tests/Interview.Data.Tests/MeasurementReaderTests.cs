using ABB.Interview.Contracts;
using ABB.Interview.Data.MapProfiles;
using ABB.Interview.Domain;
using AutoMapper;

namespace ABB.Interview.Data.Tests
{
    public class MeasurementReaderTests
    {
        private readonly IMeasurementReader _reader;
        private const string _measurementsFilePath = "D:\\Source\\Repos\\enervalis\\dotnet-measurements\\src\\Interview.API\\data\\measurements.json";

        public MeasurementReaderTests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DeviceMaps());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _reader = new MeasurementReader(mapper);
        }

        [Fact]
        public async Task Throws_If_FileNotFound()
        {
            string filePath = "d:\\temp\\doesnotexist.json";
            ApplicationException exception = await Assert.ThrowsAsync<ApplicationException>(() => _reader.ReadAsync(filePath));
            Assert.Contains("Measurement file not found", exception.Message);
        }

        [Fact]
        public async Task Can_Read_Devices()
        {
            var result = await _reader.ReadAsync(_measurementsFilePath);
            Assert.IsType<Dictionary<Device, Power[]>>(result);
            Assert.True(result.Any());
        }

        [Fact]
        public async Task Devices_Have_Power()
        {
            var result = await _reader.ReadAsync(_measurementsFilePath);
            Assert.IsType<Dictionary<Device, Power[]>>(result);
            Assert.True(result.All(d => d.Value.Any()));
        }
    }
}