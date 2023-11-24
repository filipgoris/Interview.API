using ABB.Interview.Domain;

namespace ABB.Interview.Contracts
{
    public interface IMeasurementReader
    {
        Task<Dictionary<Device, Power[]>?> ReadAsync(string measurementFilePath, CancellationToken cancellationToken);
    }
}