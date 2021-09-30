using System.Collections.Generic;
using ItemPriceWatcher.Data.Models;

namespace ItemPriceWatcher.Data.Access
{
    public interface IWatchItemLogAccess
    {
        WatchItemLog GetMostRecentLogForWatchItemID(int id);
        int InsertWatchItemLog(WatchItemLog log);
    }
}