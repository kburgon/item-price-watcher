using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ItemPriceWatcher.Data.Access;
using ItemPriceWatcher.Data.Models;
using Microsoft.Extensions.Logging;

namespace ItemPriceWatcher.BusinessLogic
{
    public class PriceWatcherWorkerLogic : IPriceWatcherWorkerLogic
    {
        private readonly ILogger<PriceWatcherWorkerLogic> _logger;
        private readonly IWatchItemAccess _watchItemAccess;
        private readonly IWatchItemLogAccess _watchItemLogAccess;
        private readonly IContactAccess _contactAccess;
        private readonly IMapperSession<WatchItem> _watchItemMapperSession;
        private readonly IMapperSession<WatchItemLog> _watchItemLogMapperSession;
        private readonly IMapperSession<Contact> _contactMapperSession;

        public PriceWatcherWorkerLogic(ILogger<PriceWatcherWorkerLogic> logger,
                                       IWatchItemAccess watchItemAccess,
                                       IWatchItemLogAccess watchItemLogAccess,
                                       IContactAccess contactAccess)
        {
            _logger = logger;
            _watchItemAccess = watchItemAccess;
            _watchItemLogAccess = watchItemLogAccess;
            _contactAccess = contactAccess;
        }
        public async Task RunAsync()
        {
            _logger.LogInformation("Getting watch items");
            IEnumerable<WatchItem> watchItems = _watchItemAccess.GetAllWatchItems();
            _logger.LogInformation($"Received {watchItems.Count()} watch item(s)");

            foreach (var watchItem in watchItems)
            {
                _logger.LogInformation($"Received item {watchItem.WatchItemName}");
                _logger.LogInformation("Getting most recent watch item log");
                WatchItemLog log = _watchItemLogAccess.GetMostRecentLogForWatchItemID(watchItem.WatchItemID);
                _logger.LogInformation($"Most recent log: Log ID {log.WatchItemLogID}, price {log.Price}");
            }
        }

        public bool ShouldRun()
        {
            throw new System.NotImplementedException();
        }

        private void RunOld()
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
                // await _watchItemLogMapperSession.SafeSaveAsync(new WatchItemLog { Price = price, LoggedAt = DateTime.Now });
            }

            _logger.LogInformation("PriceWatcherService running at: {time}", DateTimeOffset.Now);
            // await Task.Delay(1000, stoppingToken);
        }
    }
}