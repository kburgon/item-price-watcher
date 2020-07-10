using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchItemData;
using WatchItemData.WatchItemAccess;
using WatchItemData.WatchItemAccess.ORM;

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
        /// Passes if data can be added and retrieved from the SQL data retriever.
        /// </summary>
        [TestMethod]
        public async Task CanRetrieveDBData()
        {
            var testItem = CreateTestItem();
            IWatchItemAccess itemAccess = new SqlWatchItemAccess(session);
            await itemAccess.Save(testItem);

            try
            {
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

        /// <summary>
        /// Passes if a test item can be saved and retrieved from the database with logs.
        /// </summary>
        [TestMethod]
        public async Task CanRetrieveLogsWithData()
        {
            var testItem = CreateTestItem();
            IWatchItemAccess itemAccess = new SqlWatchItemAccess(session);
            await itemAccess.Save(testItem);

            try
            {
                List<WatchItem> watchItems = itemAccess.GetWatchItems();
                Assert.AreEqual(testItem.WatchItemLogs.Last().Price, watchItems.Last().WatchItemLogs.Last().Price);
            }
            finally
            {
                session.BeginTransaction();
                await session.Delete(testItem);
                await session.Commit();
                session.CloseTransaction();
            }
        }

        /// <summary>
        /// Passes if a WatchItem object can add a new log and it persists to the database.
        /// </summary>
        [TestMethod]
        public async Task CanUpdateLogsWithData()
        {
            var access = new SqlWatchItemAccess(session);
            var testItem = CreateTestItem();
            await access.Save(testItem);

            try
            {
                await access.AddLog(testItem, new WatchItemLog
                {
                    LoggedAt = DateTime.Now,
                    Price = 12.34M
                });

                var results = access.GetWatchItems();
                Assert.IsTrue(results.Last().WatchItemLogs.Count == 2);
            }
            finally
            {
                session.BeginTransaction();
                await session.Delete(testItem);
                await session.Commit();
                session.CloseTransaction();
            }
        }

        private WatchItem CreateTestItem()
        {
            var item = new WatchItem
            {
                WatchItemName = "test",
                WebsiteUrl = "https://www.google.com",
                ItemPath = "/html",
                WatchItemLogs = new List<WatchItemLog> { }
            };

            var log = new WatchItemLog
            {
                LoggedAt = DateTime.Now,
                Price = 12.55M
            };

            item.AddLog(log);
            return item;
        }
    }
}
