using System;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WatchItemData;

namespace WatchItemdata.WatchItemAccess.ORM.SqlMaps
{
    public class WatchItemMap : ClassMapping<WatchItem>
    {
        public WatchItemMap()
        {
            Id(x => x.WatchItemID, x => {
                x.Generator(Generators.Identity);
                x.Type(NHibernateUtil.Int32);
                x.UnsavedValue(0);
            });

            Property(w => w.WatchItemName, w => 
            {
                w.Length(30);
                w.Type(NHibernateUtil.StringClob);
                w.NotNullable(true);
            });

            Property(w => w.WebsiteUrl, w => 
            {
                w.Length(200);
                w.Type(NHibernateUtil.StringClob);
                w.NotNullable(true);
            });

            Property(w => w.ItemPath, w => 
            {
                w.Length(200);
                w.Type(NHibernateUtil.StringClob);
                w.NotNullable(true);
            });
        }
    }
}