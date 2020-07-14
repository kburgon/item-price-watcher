using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Linq;
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
                serviceScope.Dispose();
                Log.CloseAndFlush();
            }
        }

        private static async System.Threading.Tasks.Task RunApplicationAsync()
        {
            Log.Information("Configuring services");
            ServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            Log.Information("Building service provider");
            IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

            Log.Information("Getting watch items");
            var session = serviceScope.ServiceProvider.GetRequiredService<IMapperSession<WatchItem>>();
            var watchItems = session.Objects.ToList();
            Log.Information($"Received {watchItems.Count()} Watch Item(s)");
            
            foreach (var item in watchItems)
            {
                Log.Information($"Received item {item.WatchItemName}");
                decimal price;
                using (var checker = new PriceCheckAction(item))
                {
                    price = checker.GetItemPrice();
                    Log.Information($"Price of {item.WatchItemName}: ${price}");
                }

                if (price < item.WatchItemLogs.Last().Price && item.Contacts.Any())
                {
                    var email = serviceScope.ServiceProvider.GetRequiredService<EmailSender>();
                    foreach (var contact in item.Contacts)
                    {
                        Log.Information($"Sending email to {contact.GetFullName()}");
                        email.SendMail(contact.Email, $@"Price Drop: {item.WatchItemName}", $@"Previous price: ${item.WatchItemLogs.Last().Price}.  Current price: ${price}.");
                    }
                }

                item.AddLog(new WatchItemLog { Price = price, LoggedAt = DateTime.Now });
                Log.Information("Adding log entry");
                await session.SafeSaveAsync(item);
                Log.Information("Log entry added");
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

            // Declare email sender singleton;
            var emailUser = configuration.GetSection("Smtp").GetValue<string>("Username");
            var emailPass = configuration.GetSection("Smtp").GetValue<string>("Password");
            var emailSender = new EmailSender(emailUser, emailPass);

            // Add access to generic IConfigurationRoot
            serviceCollection.AddSingleton<IConfigurationRoot>(configuration);

            // Add service scope
            var host = Host.CreateDefaultBuilder()
                .ConfigureServices(services => 
                {
                    var connectionString = configuration.GetConnectionString("Development");
                    services.AddNHibernate<WatchItem>(connectionString);
                    services.AddSingleton<EmailSender>(emailSender);
                })
                .Build();
            serviceScope = host.Services.CreateScope();
        }
    }
}
