using WatchItemData;
using WatchItemData.WatchItemAccess;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

namespace ItemPriceWatcher.Test.Mocks
{
    internal class MockWatchItemAccess : IWatchItemAccess
    {
        public List<WatchItem> GetWatchItems()
        {
            return new List<WatchItem>
            {
                new WatchItem
                {
                    WatchItemID = 1,
                    WatchItemName = "TestItem1",
                    WebsiteUrl = @"https://www.google.com",
                    ItemPath = @"html"
                }
            };
        }

        public Task Save(WatchItem testItem)
        {
            throw new NotImplementedException();
        }
    }
}