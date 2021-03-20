using ItemPriceWatcher.Manager.Models.WatchItem;
using WatchItemData;

namespace ItemPriceWatcher.Manager.BLL
{
    public interface IWatchItemManager
    {
        WatchItemCollectionViewModel GetWatchItems();
        void AddWatchItem(WatchItem watchItem, Contact contact);
    }
}