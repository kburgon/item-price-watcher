using System.Threading.Tasks;
using ItemPriceWatcher.Data.Models;

namespace ItemPriceWatcher.Data.Access
{
    public interface IPriceAccess
    {
        Task<decimal> GetPriceAsync(WatchItem item);
    }
}