using System;
using System.Collections.Generic;

namespace WatchItemData.WatchItemAccess
{
    public interface IWatchItemAccess
    {
        List<WatchItem> GetActiveWatchItems();
    }
}
