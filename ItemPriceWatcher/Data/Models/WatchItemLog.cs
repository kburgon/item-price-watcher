using System;
using FluentNHibernate.Mapping;

namespace ItemPriceWatcher.Data.Models
{
    public class WatchItemLog
    {
        public virtual int WatchItemLogID { get; set; }
        public virtual int WatchItemID { get; set; }
        public virtual DateTime LoggedAt { get; set; }
        public virtual decimal Price { get; set; }
    }

    public class WatchItemLogMap : ClassMap<WatchItemLog>
    {
        public WatchItemLogMap()
        {
            Id(x => x.WatchItemLogID).GeneratedBy.Identity();
            Map(p => p.WatchItemID);
            Map(p => p.LoggedAt);
            Map(p => p.Price);
        }
    }
}