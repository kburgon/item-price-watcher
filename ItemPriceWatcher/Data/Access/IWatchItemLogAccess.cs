using System.Collections.Generic;
using ItemPriceWatcher.Data.Models;

namespace ItemPriceWatcher.Data.Access
{
    public interface IWatchItemLogAccess
    {
        IEnumerable<WatchItemLog> GetWatchItemLogsForWatchItemId(int id);
        int InsertWatchItemLog(WatchItemLog log);
    }
}