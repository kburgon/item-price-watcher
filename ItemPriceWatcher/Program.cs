using System;
using System.Linq;
using WatchItemData.WatchItemAccess;

namespace ItemPriceWatcher
{
    class Program
    {
        // static IWatchItemAccess itemGet;

        static void Main(string[] args)
        {
            string username = string.Empty;
            string password = string.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-u")
                {
                    username = args[i+1];
                }
                else if (args[i] == "-p")
                {
                    password = args[i+1];
                }
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("A username or password is required.");
                return;
            }

            EmailSender sender = new EmailSender(username, password);
            sender.SendMail(username, "TEST EMAIL", "Hello, world!");
        }
    }
}
