using ItemPriceWatcher.Data.Models;
using System.Collections.Generic;

namespace ItemPriceWatcher.Data.Access
{
    public interface IContactAccess
    {
        IEnumerable<Contact> GetContactsForWatchItemId(int id);
        int InsertContact(Contact contact);
    }
}