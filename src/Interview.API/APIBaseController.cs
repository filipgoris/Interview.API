using ABB.Interview.Contracts;
using ABB.Interview.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ABB.Interview.API
{
    public class APIBaseController : ControllerBase
    {
        private readonly IMeasurementReader _reader;
        private string _measurementFilePath;

        public APIBaseController(IMeasurementReader reader, IConfiguration config)
        {
            _measurementFilePath = config.GetValue<string>("Data:MeasurementFilePath");
            _reader = reader;
        }

        protected async Task<Dictionary<Device, Power[]>?> GetMeasures(CancellationToken cancellation) =>
            await _reader.ReadAsync(_measurementFilePath, cancellation);
    }
}
