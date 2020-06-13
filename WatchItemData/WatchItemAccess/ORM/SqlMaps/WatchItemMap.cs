using FluentNHibernate.Mapping;
using NHibernate;
using NHibernate.Mapping.ByCode;
using WatchItemData;

namespace WatchItemdata.WatchItemAccess.ORM.SqlMaps
{
    public class WatchItemMap : ClassMap<WatchItem>
    {
        public WatchItemMap()
        {
            Id(x => x.WatchItemID).GeneratedBy.Identity();
            Map(p => p.WatchItemName).Default(string.Empty).Length(30);
            Map(p => p.WebsiteUrl).Default(string.Empty).Length(200);
            Map(p => p.ItemPath).Default(string.Empty).Length(200);
            HasMany<WatchItemLog>(w => w.WatchItemLogs)
                .Inverse()
                .KeyColumns.Add("WatchItemID", mapping => mapping.Name("WatchItemID"));
        }
    }
}