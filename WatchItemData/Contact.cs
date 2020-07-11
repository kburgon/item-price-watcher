using FluentNHibernate.Mapping;

namespace WatchItemData
{
    /// <summary>
    /// Object representing a record in the Concact DB table.
    /// </summary>
    public class Contact
    {
        public virtual int ContactID { get; set; }
        public virtual int WatchItemID { get; set; }
        public virtual WatchItem WatchItem { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Email { get; set; }
    }

    /// <summary>
    /// Extension methods to the Conctact object.
    /// </summary>
    public static class ContactExtensions
    {
        /// <summary>
        /// Gets the full name of the contact.
        /// </summary>
        /// <param name="contact">The extensible contact.</param>
        /// <returns>The full name of the <paramref name="contact"/>.</returns>
        public static string GetFullName(this Contact contact) 
            => $"{contact.FirstName} {contact.Surname}";
    }

    /// <summary>
    /// Map class which maps the Contact object to its associated DB table.
    /// </summary>
    public class ContactMap : ClassMap<Contact>
    {
        public ContactMap()
        {
            Id(x => x.ContactID).GeneratedBy.Identity();
            Map(p => p.FirstName);
            Map(p => p.Surname);
            Map(p => p.Email);
            References(w => w.WatchItem)
                .Class<WatchItem>()
                .Columns("WatchItemID");
        }
    }
}