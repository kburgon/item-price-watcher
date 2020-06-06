using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WatchItemData;
using WatchItemData.WatchItemAccess;
using ItemPriceWatcher.Test.Mocks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WatchItemData.WatchItemAccess.ORM;
using WatchItemData.WatchItemAccess.ORM.Sessions;
using System.Threading.Tasks;

namespace ItemPriceWatcher.Test
{
    [TestClass]
    public class WatchItemDataTests
    {
        private const string CONN_STRING = @"Server=127.0.0.1; Database=ItemPriceWatcher; Port=3308; Uid=kevin; Pwd=co1ch2ito3";
        private static IServiceScope serviceScope;
        private static IMapperSession<WatchItem> session;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            var host = Host.CreateDefaultBuilder()
                            .ConfigureServices(services => 
                            {
                                services.AddNHibernate<WatchItem>(CONN_STRING);
                            })
                            .Build();
            serviceScope = host.Services.CreateScope();
            session = serviceScope.ServiceProvider.GetRequiredService<IMapperSession<WatchItem>>();
        }

        [ClassCleanup]
        public static void ClassDestruct() => serviceScope.Dispose();

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

        /// <summary>
        /// Passes if data can be added and retrieved from the SQL data retriever.
        /// </summary>
        [TestMethod]
        public async Task CanRetrieveDBData()
        {
            var testItem = new WatchItem
            {
                WatchItemName = "test",
                WebsiteUrl = "https://www.google.com",
                ItemPath = "/html"
            };

            session.BeginTransaction();
            await session.Save(testItem);
            await session.Commit();
            session.CloseTransaction();

            try
            {
                IWatchItemAccess itemAccess = new SqlWatchItemAccess(session);
                List<WatchItem> watchItems = itemAccess.GetWatchItems();
                Assert.IsTrue(watchItems.Count > 0);
            }
            finally
            {
                session.BeginTransaction();
                await session.Delete(testItem);
                await session.Commit();
                session.CloseTransaction();
            }
        }
    }
}
