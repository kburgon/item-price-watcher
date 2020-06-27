using FluentNHibernate.Mapping;

namespace WatchItemData.WatchItemAccess.ORM.SqlMaps
{
    public class WatchItemLogMap : ClassMap<WatchItemLog>
    {
        public WatchItemLogMap()
        {
            Id(x => x.WatchItemLogID).GeneratedBy.Identity();
            Map(p => p.LoggedAt);
            Map(p => p.Price);
            References(w => w.WatchItem)
                .Class<WatchItem>()
                // .Cascade.All()
                .Columns("WatchItemID");
        }
    }
}