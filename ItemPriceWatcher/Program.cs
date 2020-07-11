using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchItemData;
using WatchItemData.ORM;

namespace ItemPriceWatcher
{
    class Program
    {
        public static IConfigurationRoot configuration;
        public static IServiceScope serviceScope;

        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Debug("Logger initialized");

            try
            {
                await RunApplicationAsync();
            }
            catch (Exception e)
            {
                Log.Error("Application error encountered", e);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static async System.Threading.Tasks.Task RunApplicationAsync()
        {
            Log.Information("Starting application");
            Log.Information("Configuring services");
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            Log.Information("Building service provider");
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            Log.Information("Getting watch items");
            var session = serviceScope.ServiceProvider.GetRequiredService<IMapperSession<WatchItem>>();
            var watchItems = session.Objects.ToList();
            Log.Information("Received Watch Items");
            
            foreach (var item in watchItems)
            {
                await NewMethod(session, item);
            }
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton(LoggerFactory.Create(builder => 
            {
                builder.AddSerilog(dispose: true);
            }));

            serviceCollection.AddLogging();

            configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                .AddJsonFile("appsettings.json", false)
                .Build();

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            // Add service scope
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => 
                {
                    var connectionString = configuration.GetConnectionString("Development");
                    services.AddNHibernate<WatchItem>(connectionString);
                    services.AddNHibernate<WatchItemLog>(connectionString);
                })
                .Build();
            serviceScope = host.Services.CreateScope();
        }

        private static async Task NewMethod(IMapperSession<WatchItem> session, WatchItem item)
        {
            Log.Information($"Received item {item.WatchItemName}.");
            var checker = new PriceCheckAction(item);
            var price = checker.GetItemPrice();
            Log.Information($"Price of {item.WatchItemName}: ${price}");

            if (price < item.WatchItemLogs.Last().Price)
            {
                using var email = new EmailSender("", "");
                foreach (var contact in item.Contacts)
                {
                    Log.Information($"Sending email to {contact.GetFullName()}");
                    email.SendMail(contact.Email, $@"Price Drop: {item.WatchItemName}", $@"Previous price: ${item.WatchItemLogs.Last().Price}.  Current price: ${price}.");
                }
            }

            item.AddLog(new WatchItemLog { Price = price, LoggedAt = DateTime.Now });
            await session.SafeSaveAsync(item);
        }
    }
}
