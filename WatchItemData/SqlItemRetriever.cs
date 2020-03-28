using System;
using System.Collections.Generic;
using SharedObjects;

namespace WatchItemStore
{
    class SqlWorkItemRetriever : IItemRetriever
    {
        public List<WatchItem> GetActiveWatchItems()
        {
            return new List<WatchItem>();
        }
    }
}