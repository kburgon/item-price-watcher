using WatchItemData;
using WatchItemData.WatchItemAccess;
using System.Collections.Generic;
using System;

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
                    Url = @"https://www.google.com",
                    ItemPath = @"html"
                }
            };
        }
    }
}