using System.Collections.Generic;
using System.Linq;
using WatchItemData.WatchItemAccess.ORM.Sessions;

namespace WatchItemData.WatchItemAccess
{
    public class SqlWatchItemAccess : IWatchItemAccess
    {
        private readonly IMapperSession<WatchItem> session;

        public SqlWatchItemAccess(IMapperSession<WatchItem> session)
        {
            this.session = session;
        }

        public List<WatchItem> GetWatchItems() => session.Objects.ToList();
    }
}