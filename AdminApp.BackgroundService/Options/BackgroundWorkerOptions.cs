namespace AdminApp.BackgroundService.Options
{
    /// <summary>
    /// Configuration options for the BackgroundWorker.
    /// </summary>
    /// <remarks>
    /// <see cref="DelayBeforeStartSeconds"/> specifies the time to wait until the initial start of the task.
    /// <see cref="DelayBetweenRunsMinutes"/> specifies the time between each task run.
    /// </remarks>
    public class BackgroundWorkerOptions
    {
        public const string BackgroundWorker = "BackgroundWorker";

        public int DelayBetweenRunsMinutes { get; init; }

        public int DelayBeforeStartSeconds { get; init; }

        public int AuditLogsToKeep { get; init; }

        public int ErrorsToKeep { get; init; }

        public int BatchSize { get; init; }
    }
}
