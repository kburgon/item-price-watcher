using System;
using System.Collections.Generic;
using SharedObjects;

namespace WatchItemData.WatchItemAccess
{
    public interface IWatchItemAccess
    {
        List<WatchItem> GetActiveWatchItems();
    }
}
