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

        public List<WatchItem> GetAllWatchItems() 
            => _watchItemSession.Objects.ToList();
    }
}