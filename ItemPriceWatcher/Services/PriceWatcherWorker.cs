using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ItemPriceWatcher.Data.Access;
using ItemPriceWatcher.Data.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ItemPriceWatcher.Services
{
    public class PriceWatcherWorker : BackgroundService
    {
        private readonly ILogger<PriceWatcherWorker> _logger;
        private readonly IMapperSession<WatchItem> _watchItemMapperSession;
        private readonly IMapperSession<WatchItemLog> _watchItemLogMapperSession;
        private readonly IMapperSession<Contact> _contactMapperSession;

        public PriceWatcherWorker(ILogger<PriceWatcherWorker> logger, 
                                    IMapperSession<WatchItem> watchItemMapperSession, 
                                    IMapperSession<WatchItemLog> logMapperSession,
                                    IMapperSession<Contact> contactMapperSession)
        {
            _logger = logger;
            _watchItemMapperSession = watchItemMapperSession;
            _watchItemLogMapperSession = logMapperSession;
            _contactMapperSession = contactMapperSession;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Getting watch itmes");
                List<WatchItem> watchItems = _watchItemMapperSession.Objects.ToList();
                _logger.LogInformation($"Received {watchItems.Count} watch item(s)");

                decimal price;
                foreach (var watchItem in watchItems)
                {
                    _logger.LogInformation($"Received item {watchItem.WatchItemName}");
                    price = default(decimal);
                    // TODO: Add the PriceCheckAction class

                    IEnumerable<WatchItemLog> logs = _watchItemLogMapperSession.Objects
                                                        .Where(log => log.WatchItemID == watchItem.WatchItemID)
                                                        .OrderByDescending(log => log.WatchItemLogID);

                    if (logs.Any() && price < logs.Last().Price)
                    {
                        IEnumerable<Contact> contacts = _contactMapperSession.Objects.Where(contact => contact.WatchItemID == watchItem.WatchItemID);
                        foreach (var contact in contacts)
                        {
                            _logger.LogInformation($"Sending email to {contact.FirstName} {contact.Surname}");
                            // TODO: Add email sending client
                        }
                    }

                    _logger.LogInformation("Adding log entry");
                    await _watchItemLogMapperSession.SafeSaveAsync(new WatchItemLog { Price = price, LoggedAt = DateTime.Now });
                }

                _logger.LogInformation("PriceWatcherWorker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
