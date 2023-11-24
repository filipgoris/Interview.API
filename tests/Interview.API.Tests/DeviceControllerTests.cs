using ABB.Interview.API.Devices.Controllers;
using ABB.Interview.API.Devices.Models;
using ABB.Interview.Contracts;
using ABB.Interview.Data;
using ABB.Interview.Data.MapProfiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Interview.API.Tests
{
    public class DeviceControllerTests
    {        
        private ILogger<DeviceController> _logger;
        private IMeasurementReader _reader;
        private const int deviceCount = 40;     // Test dataset has 40 devices;

        public DeviceControllerTests()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DeviceMaps());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            _reader = new MeasurementReader(mapper);

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _logger = factory!.CreateLogger<DeviceController>();            
        }

        [Fact]
        public async Task Get_ShouldFailGracefully_IfBadMeasuresFile()
        {
            var controller = new DeviceController(_logger, _reader, ConfigHelper.CreateConfig("badFile.json"));

            var test = await controller.Get(default);
            var result = test as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldFailGracefully_IfNoMeasuresFile()
        {
            var controller = new DeviceController(_logger, _reader, ConfigHelper.CreateConfig("noFile.json"));

            var test = await controller.Get(default);
            var result = test as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);            
        }

        [Fact]
        public async Task Get_ShouldReturn_AllDevices()
        {
            var controller = new DeviceController(_logger, _reader, ConfigHelper.CreateConfig("appsettings.json"));

            var test = await controller.Get(default);

            Assert.IsType<OkObjectResult>(test);
            var result = test as OkObjectResult;
            Assert.NotNull(result);
            var value = result.Value;
            Assert.IsAssignableFrom<IEnumerable<DeviceListModel>>(value);
            var devices = result.Value as IEnumerable<DeviceListModel>;
            Assert.NotNull(devices);
            Assert.Equal(deviceCount, devices.Count());
        }

        [Fact]
        public async Task GetMax_ShouldFailGracefully_IfNoMeasuresFile()
        {
            var controller = new DeviceController(_logger, _reader, ConfigHelper.CreateConfig("noFile.json"));

            var test = await controller.GetMax(default);
            var result = test as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetMax_ShouldFailGracefully_IfBadMeasuresFile()
        {
            var controller = new DeviceController(_logger, _reader, ConfigHelper.CreateConfig("badFile.json"));

            var test = await controller.GetMax(default);
            var result = test as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task GetMax_ShouldReturn_AllDevices()
        {
            var controller = new DeviceController(_logger, _reader, ConfigHelper.CreateConfig("appsettings.json"));

            var test = await controller.GetMax(default);

            Assert.IsType<OkObjectResult>(test);
            var result = test as OkObjectResult;
            Assert.NotNull(result);
            var value = result.Value;
            Assert.IsAssignableFrom<IEnumerable<DeviceListModelMax>>(value);
            var devices = result.Value as IEnumerable<DeviceListModelMax>;
            Assert.NotNull(devices);
            Assert.Equal(deviceCount, devices.Count());
        }
    }
}