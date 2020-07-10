using System.Linq;
using System.Threading.Tasks;
using NHibernate;

namespace WatchItemData.WatchItemAccess.ORM
{
    /// <summary>
    /// Session for mapping the object <typeref name="T"/> to the database.
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

        /// <inheritdoc/>
        public Task SafeSaveAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public Task SafeDeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IQueryable<T> Objects => session.Query<T>();

        /// <inheritdoc/>
        public void BeginTransaction() => transaction = session.BeginTransaction();

        /// <inheritdoc/>
        public async Task Commit() => await transaction.CommitAsync();

        /// <inheritdoc/>
        public async Task Rollback() => await transaction.RollbackAsync();

        /// <inheritdoc/>
        public async Task Save(T entity) => await session.SaveAsync(entity);

        /// <inheritdoc/>
        public async Task Delete(T entity) => await session.DeleteAsync(entity);

        /// <inheritdoc/>
        public void CloseTransaction()
        {
            transaction?.Dispose();
            transaction = null;
        }
    }
}