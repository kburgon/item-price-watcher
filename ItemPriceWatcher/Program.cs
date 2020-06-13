using WatchItemData.WatchItemAccess;

namespace ItemPriceWatcher
{
    class Program
    {
        // static IWatchItemAccess itemGet;

        static void Main(string[] args)
        {
            if (args.Length > 1 && args[1] == "--txtsource")
            {
                // itemGet = new SqlWatchItemAccess();
            }

            SendTestEmail(@"Hello, world!", "buburgo@gmail.com");
        }

        private static void SendTestEmail(string message, string emailAddress)
        {
            var sender = new EmailSender("buburgo@gmail.com", "7b6u1b9b9y3");
            sender.SendMail(emailAddress, "TEST EMAIL", message);
        }
    }
}
