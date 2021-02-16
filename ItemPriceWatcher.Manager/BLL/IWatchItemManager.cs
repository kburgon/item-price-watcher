using ItemPriceWatcher.Manager.Models.WatchItem;

namespace ItemPriceWatcher.Manager.BLL
{
    public interface IWatchItemManager
    {
        WatchItemCollectionViewModel GetWatchItems();
    }
}