using ItemPriceWatcher.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ItemPriceWatcher.Data.Access
{
    public interface IContactAccess
    {
        IEnumerable<Contact> GetContactsForWatchItemId(int id);
        Task<int> InsertContactAsync(Contact contact);
    }
}