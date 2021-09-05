using System.Linq;
using System.Threading.Tasks;

namespace ItemPriceWatcher.Data.Access
{
    /// <summary>
    /// Provides session actions to perform CRUD actions with the type <typeparamref name="T"/> to the database.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMapperSession<T>
    {
        /// <summary>
        /// Saves the given <paramref name="entity"/> to the database, rolling back the transaction if an exception is encountered.
        /// </summary>
        /// <param name="entity">The object to save to the database.</param>
        Task SafeSaveAsync(T entity);
        /// <summary>
        /// Removes the given <paramref name="entity"/> from the database, rolling back the transaction if an exception is encountered.
        /// </summary>
        /// <param name="entity">The object to remove from the database.</param>
        Task SafeDeleteAsync(T entity);

        /// <summary>
        /// Begins the database transaction.
        /// </summary>
        void BeginTransaction();
        /// <summary>
        /// Finalizes the transaction to the database.
        /// </summary>
        Task Commit();
        /// <summary>
        /// Rolls back the database to the state that it was in before the transaction was started.
        /// </summary>
        Task Rollback();
        /// <summary>
        /// Closes the database transaction.
        /// </summary>
        void CloseTransaction();
        /// <summary>
        /// Saves the <paramref name="entity"/> to the database (<c>INSERT</c> statement).
        /// </summary>
        /// <param name="entity">The object to save to the database.</param>
        Task Save(T entity);
        /// <summary>
        /// Removes the <paramref name="entity"/> from the database (<c>DELETE</c> statement).
        /// </summary>
        /// <param name="entity">The object to remove from the database.</param>
        Task Delete(T entity);

        /// <summary>
        /// Retrieves a queryable list of the database object <typeparamref name="T"/>.
        /// </summary>
        /// <value>The objects of type <typeparamref name="T"/> from the database.</value>
        IQueryable<T> Objects {get;}
    }
}