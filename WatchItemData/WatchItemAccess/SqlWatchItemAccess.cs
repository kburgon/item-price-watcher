using System.Collections.Generic;

namespace WatchItemData.WatchItemAccess
{
    public class SqlWatchItemAccess : IWatchItemAccess
    {
        public List<WatchItem> GetActiveWatchItems()
        {
            return new List<WatchItem>();
        }
    }
}