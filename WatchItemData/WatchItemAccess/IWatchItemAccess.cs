using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WatchItemData.WatchItemAccess
{
    public interface IWatchItemAccess
    {
        List<WatchItem> GetWatchItems();
        Task Save(WatchItem testItem);
    }
}
