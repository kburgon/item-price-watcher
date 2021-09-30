using System.Collections.Generic;
using ItemPriceWatcher.Data.Models;

namespace ItemPriceWatcher.Data.Access
{
    public interface IWatchItemAccess
    {
        IEnumerable<WatchItem> GetAllWatchItems();
    }
}