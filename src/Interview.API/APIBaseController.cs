using ABB.Interview.Contracts;
using ABB.Interview.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ABB.Interview.API
{
    public class APIBaseController : ControllerBase
    {
        private readonly IMeasurementReader _reader;

        public APIBaseController(IMeasurementReader reader)
        {
            _reader = reader;
        }

        protected async Task<Dictionary<Device, Power[]>?> GetMeasures(CancellationToken cancellation) =>
            await _reader.ReadAsync("data\\measurements.json", cancellation);
    }

}
