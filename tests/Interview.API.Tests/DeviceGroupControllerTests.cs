using ABB.Interview.API.DeviceGroups.Controllers;
using ABB.Interview.API.DeviceGroups.Models;
using ABB.Interview.Contracts;
using ABB.Interview.Data;
using ABB.Interview.Data.MapProfiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Interview.API.Tests
{
    public class DeviceGroupControllerTests
    {
        private ILogger<DeviceGroupController> _logger;
        private IMeasurementReader _reader;
        private const int groupCount = 4;    // Test dataset has 4 groups

        public DeviceGroupControllerTests()
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
            _logger = factory!.CreateLogger<DeviceGroupController>();

        }

        [Fact]
        public async Task Get_ShouldFailGracefully_IfBadMeasuresFile()
        {
            var controller = new DeviceGroupController(_logger, _reader, ConfigHelper.CreateConfig("badFile.json"));

            var test = await controller.Get(default);
            var result = test as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldFailGracefully_IfNoMeasuresFile()
        {
            var controller = new DeviceGroupController(_logger, _reader, ConfigHelper.CreateConfig("noFile.json"));

            var test = await controller.Get(default);
            var result = test as ObjectResult;

            Assert.NotNull(result);
            Assert.Equal(500, result.StatusCode);
        }

        [Fact]
        public async Task Get_ShouldReturn_AllGroups()
        {
            var controller = new DeviceGroupController(_logger, _reader, ConfigHelper.CreateConfig("appsettings.json"));

            var test = await controller.Get(default);

            Assert.IsType<OkObjectResult>(test);
            var result = test as OkObjectResult;
            Assert.NotNull(result);
            var value = result.Value;
            Assert.IsAssignableFrom<IEnumerable<DeviceGroupListModel>>(value);
            var devices = result.Value as IEnumerable<DeviceGroupListModel>;
            Assert.NotNull(devices);
            Assert.Equal(groupCount, devices.Count());
        }
    }
}
