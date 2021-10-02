using System.Collections.Generic;
using System.Threading.Tasks;
using ItemPriceWatcher.Data.Models;

namespace ItemPriceWatcher.BusinessLogic
{
    public interface INotificationSender
    {
        Task SendNotificationAsync(WatchItem watchItem, IEnumerable<Contact> contacts);
    }
}