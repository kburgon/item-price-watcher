using System.Threading.Tasks;

namespace ItemPriceWatcher.BusinessLogic
{
    public interface IPriceWatcherWorkerLogic
    {
        /// <summary>
        /// Determines if the price watcher worker logic should run.
        /// </summary>
        /// <returns>True if the logic should run, false if not.</returns>
        bool ShouldRun();

        /// <summary>
        /// Runs the main business logic of the price watcher worker.
        /// </summary>
        /// <returns>Task related to async method.</returns>
        Task RunAsync();
    }
}