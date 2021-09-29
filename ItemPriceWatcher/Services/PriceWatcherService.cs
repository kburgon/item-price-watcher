using System.Threading;
using System.Threading.Tasks;
using ItemPriceWatcher.BusinessLogic;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ItemPriceWatcher.Services
{
    public class PriceWatcherService : BackgroundService
    {
        private readonly ILogger<PriceWatcherService> _logger;
        private readonly IPriceWatcherWorkerLogic _priceWatcherWorkerLogic;

        public PriceWatcherService(ILogger<PriceWatcherService> logger, IPriceWatcherWorkerLogic priceWatcherWorkerLogic)
        {
            _logger = logger;
            _priceWatcherWorkerLogic = priceWatcherWorkerLogic;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_priceWatcherWorkerLogic.ShouldRun())
                {
                    await _priceWatcherWorkerLogic.RunAsync();
                }
            }
        }
    }
}
