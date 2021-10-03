using System.Threading.Tasks;
using ItemPriceWatcher.Data.Models;

namespace ItemPriceWatcher.Data.Access
{
    public interface IWatchItemLogAccess
    {
        WatchItemLog GetMostRecentLogForWatchItemID(int id);
        Task<int> InsertWatchItemLogAsync(WatchItemLog log);
    }
}