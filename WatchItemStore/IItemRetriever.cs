using System;
using System.Collections.Generic;
using SharedObjects;

namespace WatchItemStore
{
    public interface IItemRetriever
    {
        List<WatchItem> GetActiveWatchItems();
    }
}
