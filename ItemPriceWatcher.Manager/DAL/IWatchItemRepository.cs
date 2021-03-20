using System.Collections.Generic;
using WatchItemData;

namespace ItemPriceWatcher.Manager.DAL
{
    public interface IWatchItemRepository
    {
        public List<WatchItem> GetAllWatchItems();
        public void AddWatchItem(WatchItem watchItem);
        public WatchItem GetWatchItemByName(string name);
    }
}