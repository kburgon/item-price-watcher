using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task AddLog(WatchItem watchItem, WatchItemLog watchItemLog)
        {
            watchItem.WatchItemLogs.Add(watchItemLog);
            session.BeginTransaction();
            await session.Save(watchItem);
            await session.Commit();
            session.CloseTransaction();
        }
    }
}