using System.Linq;
using ItemPriceWatcher.Manager.DAL;
using ItemPriceWatcher.Manager.Models.WatchItem;
using WatchItemData;

namespace ItemPriceWatcher.Manager.BLL
{
    public class WatchItemManager : IWatchItemManager
    {
        private readonly IWatchItemRepository _watchItemRepository;

        public WatchItemManager(IWatchItemRepository watchItemRepository)
        {
            _watchItemRepository = watchItemRepository;
        }

        public WatchItemCollectionViewModel GetWatchItems()
        {
            var collection = new WatchItemCollectionViewModel();
            var watchItems = _watchItemRepository.GetAllWatchItems();
            foreach (var watchItem in watchItems)
            {
                WatchItemViewModel model = CreateModel(watchItem);
                collection.WatchItems.Add(model);
            }

            return collection;
        }

        private static WatchItemViewModel CreateModel(WatchItem watchItem)
        {
            var model = new WatchItemViewModel();
            var contact = watchItem.Contacts.First();
            
            if (contact != null)
            {
                model.ContactId = contact.ContactID;
                model.ContactFirstName = contact.FirstName;
                model.ContactLastName = contact.Surname;
                model.ContactEmail = contact.Email;
            }

            model.WatchItemId = watchItem.WatchItemID;
            model.WatchItemName = watchItem.WatchItemName;
            model.URL = watchItem.WebsiteUrl;
            model.ItemPath = watchItem.ItemPath;
            return model;
        }
    }
}