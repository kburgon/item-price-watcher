using System;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;

namespace ItemPriceWatcher.Data.Access
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
        public async Task SafeSaveAsync(T entity) 
            => await PerformSafeTransactionAsync(async () => await Save(entity));

        /// <inheritdoc/>
        public async Task SafeDeleteAsync(T entity) 
            => await PerformSafeTransactionAsync(async () => await Delete(entity));

        private async Task PerformSafeTransactionAsync(Action action)
        {
            try
            {
                BeginTransaction();
                action();
                await Commit();
            }
            catch
            {
                await Rollback();
                throw;
            }
            finally
            {
                CloseTransaction();
            }
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