using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

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
        public virtual IList<Contact> Contacts { get; set; }
    }

    public static class WatchItemExtensions
    {
        public static void AddLog(this WatchItem item, WatchItemLog log)
        {
            log.WatchItem = item;
            item.WatchItemLogs.Add(log);
        }

        public static void AddContact(this WatchItem item, Contact contact)
        {
            contact.WatchItem = item;
            item.Contacts.Add(contact);
        }
    }

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
                .Cascade.AllDeleteOrphan()
                .Fetch.Join().KeyColumn("WatchItemID")
                .AsList();
            HasMany<Contact>(w => w.Contacts)
                .Inverse()
                .Cascade.AllDeleteOrphan()
                .Fetch.Join().KeyColumn("ContactID")
                .AsList();
        }
    }
}
