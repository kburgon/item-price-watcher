using System.Collections.Generic;

namespace WatchItemData.WatchItemAccess
{
    public class SqlWatchItemAccess : IWatchItemAccess
    {
        public List<WatchItem> GetWatchItems()
        {
            return new List<WatchItem>();
        }
    }
}