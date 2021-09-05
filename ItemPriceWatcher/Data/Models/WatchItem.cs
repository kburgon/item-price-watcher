using FluentNHibernate.Mapping;

namespace ItemPriceWatcher.Data.Models
{
    public class WatchItem
    {
        public virtual int WatchItemID { get; set; }
        public virtual string WatchItemName { get; set; }
        public virtual string WebsiteUrl { get; set; }
        public virtual string ItemPath { get; set; }
    }

    public class WatchItemMap : ClassMap<WatchItem>
    {
        public WatchItemMap()
        {
            Id(x => x.WatchItemID).GeneratedBy.Identity();
            Map(p => p.WatchItemName).Length(30);
            Map(p => p.WebsiteUrl).Length(200);
            Map(p => p.ItemPath).Length(200);
        }
    }
}