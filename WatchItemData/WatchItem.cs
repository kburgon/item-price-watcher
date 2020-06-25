using System;
using System.Collections.Generic;

namespace WatchItemData
{
    [Serializable]
    public class WatchItem
    {
        public virtual int WatchItemID { get; set; }
        public virtual string WatchItemName { get; set; }
        public virtual string WebsiteUrl { get; set; }
        public virtual string ItemPath { get; set; }
        public virtual IList<WatchItemLog> WatchItemLogs { get; set; }
    }
}
