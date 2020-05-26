using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WatchItemData;
using WatchItemData.WatchItemAccess;
using ItemPriceWatcher.Test.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WatchItemData.WatchItemAccess.ORM.Extensions;
using WatchItemData.WatchItemAccess.ORM.Sessions;

namespace ItemPriceWatcher.Test
{
    [TestClass]
    public class WatchItemDataTests
    {
        private const string CONN_STRING = @"Server=127.0.0.1; Database=ItemPriceWatcher; Port=3308; Uid=kevin; Pwd=co1ch2ito3";
        private static IMapperSession<WatchItem> session;

        [AssemblyInitialize]
        public static void InitializeAssembly(TestContext context)
        {
            var host = Host.CreateDefaultBuilder()
                            .ConfigureServices(services => 
                            {
                                services.AddNHibernate<WatchItem>(CONN_STRING);
                            })
                            .Build();
            using var serviceScope = host.Services.CreateScope();
            session = serviceScope.ServiceProvider.GetRequiredService<IMapperSession<WatchItem>>();
        }

        /// <summary>
        /// Passes if data can be retrieved from the test data retriever.
        /// </summary>
        [TestMethod]
        public void CanRetreiveMockData()
        {
            IWatchItemAccess itemAccess = new MockWatchItemAccess();
            List<WatchItem> watchItems = itemAccess.GetWatchItems();
            Assert.IsTrue(watchItems.Count > 0);
        }

        [TestMethod]
        public void CanRetrieveDBData()
        {
            IWatchItemAccess itemAccess = new SqlWatchItemAccess(session);
            List<WatchItem> watchItems = itemAccess.GetWatchItems();
            Assert.IsTrue(watchItems.Count > 0);
        }
    }
}
