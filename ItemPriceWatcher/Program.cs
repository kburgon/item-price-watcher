using WatchItemData.WatchItemAccess;

namespace ItemPriceWatcher
{
    class Program
    {
        static IWatchItemAccess itemGet;

        static void Main(string[] args)
        {
            if (args.Length > 1 && args[1] == "--txtsource")
            {
                // itemGet = new SqlWatchItemAccess();
            }
        }
    }
}
