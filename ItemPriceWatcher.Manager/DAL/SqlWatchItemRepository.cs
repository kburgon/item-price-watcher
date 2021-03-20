using System.Collections.Generic;
using System.Linq;
using WatchItemData;
using WatchItemData.ORM;

namespace ItemPriceWatcher.Manager.DAL
{
    public class SqlWatchItemRepository : IWatchItemRepository
    {
        private readonly IMapperSession<WatchItem> _watchItemSession;

        public SqlWatchItemRepository(IMapperSession<WatchItem> session)
        {
            _watchItemSession = session;
        }

        public void AddWatchItem(WatchItem watchItem) 
            => _watchItemSession.SafeSaveAsync(watchItem).Wait();

        public List<WatchItem> GetAllWatchItems() 
            => _watchItemSession.Objects.ToList();

        public WatchItem GetWatchItemByName(string name) 
            => _watchItemSession.Objects.FirstOrDefault(watchItem => watchItem.WatchItemName == name);
    }
}