using ItemPriceWatcher.Manager.DAL;
using ItemPriceWatcher.Manager.Models.WatchItem;

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
                collection.WatchItems.Add(new WatchItemViewModel
                    {
                        WatchItemId = watchItem.WatchItemID,
                        WatchItemName = watchItem.WatchItemName,
                        URL = watchItem.WebsiteUrl,
                        ItemPath = watchItem.ItemPath
                    });
            }

            return collection;
        }
    }
}