using System;
using WatchItemData;

namespace ItemPriceWatcher
{
    class Program
    {
        static IWorkItemAccess itemGet;

        static void Main(string[] args)
        {
            if (args.Length > 1 && args[1] == "--txtsource")
            {
                itemGet = new SqlWorkItemAccess();
            }
        }
    }
}
