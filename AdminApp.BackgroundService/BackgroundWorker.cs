using AdminApp.BackgroundService.Options;
using AdminApp.Data.Common;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AdminApp.BackgroundService
{
    // sc delete "Admin Application Background Service"
    // sc create "Admin Application Background Service" binPath=C:\Dev\Git\admin-application\AdminApp.BackgroundService\bin\Debug\net5.0\AdminApp.BackgroundService.exe

    public class BackgroundWorker : IHostedService, IDisposable
    {
        private readonly ILogger<BackgroundWorker> _logger;
        private readonly ConnectionStringsOptions _connectionStringsOptions;
        private readonly BackgroundWorkerOptions _backgroundWorkerOptions;

        private bool disposedValue;
        private Timer _timer;

        public IServiceProvider Services { get; init; }

        public BackgroundWorker(
            ILogger<BackgroundWorker> logger,
            IOptions<ConnectionStringsOptions> connectionStringsOptions,
            IOptions<BackgroundWorkerOptions> backgroundWorkerOptions,
            IServiceProvider services)
        {
            _logger = logger;

            _connectionStringsOptions = connectionStringsOptions.Value;
            _backgroundWorkerOptions = backgroundWorkerOptions.Value;

            Services = services;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Worker started running at: {time}", DateTimeOffset.Now);

            _timer = new(DoWork, null, TimeSpan.FromSeconds(_backgroundWorkerOptions.DelayBeforeStartSeconds), TimeSpan.FromMinutes(_backgroundWorkerOptions.DelayBetweenRunsMinutes));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            try
            {
                CleanupItems();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to clean up items", DateTimeOffset.Now);
            }
        }

        private void CleanupItems()
        {
            //Stopwatch stopwatch = Stopwatch.StartNew();
            //stopwatch.Start();

            //CleanupItems<Error>(_backgroundWorkerOptions.ErrorsToKeep);
            //CleanupItems<AuditLog>(_backgroundWorkerOptions.AuditLogsToKeep);

            //stopwatch.Stop();

            //_logger.LogInformation($"CleanupItems ran for {stopwatch.ElapsedMilliseconds}ms", DateTimeOffset.Now);
        }

        /// <summary>
        /// Keeps the top x items. See <see cref="BackgroundWorkerOptions"/> for configuration options.
        /// </summary>
        /// <typeparam name="T">The entity type to cleanup</typeparam>
        private void CleanupItems<T>(int itemsToKeep = 1000)
            where T : DomainModel
        {
            //using AdminAppContext db = new(_connectionStringsOptions.DefaultConnection);

            //int totalItems = db.Set<T>().Count();
            //int totalDeleted = default;

            //while (totalItems > itemsToKeep)
            //{
            //    var idsToDelete = db.Set<T>()
            //        .AsNoTracking()
            //        .OrderByDescending(x => x.Id)
            //        .Skip(itemsToKeep)
            //        .Take(_backgroundWorkerOptions.BatchSize)
            //        .Select(x => x.Id)
            //        .ToList();

            //    if (!idsToDelete.Any())
            //    {
            //        return;
            //    }

            //    db.Database.ExecuteSqlCommand($"DELETE FROM {typeof(T)?.Name} WHERE Id IN ({string.Join(',', idsToDelete)})");

            //    db.SaveChanges();

            //    totalDeleted += idsToDelete.Count;

            //    totalItems = db.Set<T>().Count();
            //}

            //_logger.LogInformation($"Removed {totalDeleted} {typeof(T)?.Name} entries.", DateTimeOffset.Now);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Worker is stopping.");

            _timer?.Change(Timeout.Infinite, Timeout.Infinite);

            return Task.CompletedTask;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _timer?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
