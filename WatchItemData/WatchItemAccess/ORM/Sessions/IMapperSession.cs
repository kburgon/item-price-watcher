using System.Linq;
using System.Threading.Tasks;

namespace WatchItemData.WatchItemAccess.ORM.Sessions
{
    public interface IMapperSession<T>
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task Save(T entity);
        Task Delete(T entity);

        IQueryable<T> Objects {get;}
    }
}