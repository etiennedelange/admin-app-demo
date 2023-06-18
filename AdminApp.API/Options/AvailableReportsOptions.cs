using System.Collections.Generic;

namespace AdminApp.API.Options
{
    public record AvailableReportsOptions : IAvailableReportsOptions
    {
        public const string AvailableReports = "AvailableReports";

        public IEnumerable<string> Reports { get; init; }
    }

    //Interface for mocking
    public interface IAvailableReportsOptions
    {
        public IEnumerable<string> Reports { get; init; }
    }
}
