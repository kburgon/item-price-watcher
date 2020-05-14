using System.Linq;
using System.Threading.Tasks;
using NHibernate;

namespace WatchItemData.WatchItemAccess.ORM.Sessions
{
    /// <summary>
    /// Session for mapping the given object <typeref name="T"/> to the database.
    /// </summary>
    /// <typeparam name="T"> The object to map to the database. </typeparam>
    public class MapperSession<T> : IMapperSession<T>
    {
        private readonly ISession session;
        private ITransaction transaction;

        /// <summary>
        /// Constructor that sets the current session to the given <paramref name="session"/>
        /// </summary>
        /// <param name="session"></param>
        public MapperSession(ISession session) => this.session = session;

        public IQueryable<T> Objects => session.Query<T>();

        public void BeginTransaction() => transaction = session.BeginTransaction();

        public async Task Commit() => await transaction.CommitAsync();

        public async Task Rollback() => await transaction.RollbackAsync();

        public async Task Save(T entity) => await session.SaveAsync(entity);

        public async Task Delete(T entity) => await session.DeleteAsync(entity);

        public void CloseTransaction()
        {
            transaction?.Dispose();
            transaction = null;
        }
    }
}