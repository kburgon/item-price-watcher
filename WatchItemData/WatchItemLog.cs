using System;

namespace WatchItemData
{
    [Serializable]
    public class WatchItemLog
    {
        public virtual int WatchItemLogID { get; set; }
        public virtual WatchItem WatchItem { get; set; }
        public virtual int WatchItemID { get; set; }
        public virtual DateTime LoggedAt { get; set; }
        public virtual decimal Price { get; set; }
    }
}