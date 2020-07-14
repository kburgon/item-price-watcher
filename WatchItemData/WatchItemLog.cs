using System;
using FluentNHibernate.Mapping;

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

    public class WatchItemLogMap : ClassMap<WatchItemLog>
    {
        public WatchItemLogMap()
        {
            Id(x => x.WatchItemLogID).GeneratedBy.Identity();
            Map(p => p.LoggedAt);
            Map(p => p.Price);
            References(w => w.WatchItem)
                .Class<WatchItem>()
                .Columns("WatchItemID")
                .LazyLoad();
        }
    }
}