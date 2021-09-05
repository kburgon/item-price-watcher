using FluentNHibernate.Mapping;

namespace ItemPriceWatcher.Data.Models
{
    public class Contact
    {
        public virtual int ContactID { get; set; }
        public virtual int WatchItemID { get; set; }
        public virtual WatchItem WatchItem { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
    }

    public class ContactMap : ClassMap<Contact>
    {
        public ContactMap()
        {
            Id(x => x.ContactID).GeneratedBy.Identity();
            Map(x => x.WatchItemID);
            Map(p => p.FirstName);
            Map(p => p.Surname);
            Map(p => p.Email);
        }
    }
}