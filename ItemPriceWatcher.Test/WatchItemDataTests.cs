using ItemPriceWatcher.Test.Mocks;
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
using WatchItemData.WatchItemAccess.ORM.Sessions;

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

        [TestMethod]
        public async Task CanRetrieveLogsWithData()
        {
            var testItem = new WatchItem
            {
                WatchItemName = "test2",
                WebsiteUrl = "https://www.google.com",
                ItemPath = "/html",
                WatchItemLogs = new List<WatchItemLog>
                {
                    new WatchItemLog
                    {
                        LoggedAt = DateTime.Now,
                        Price = 12.55M
                    }
                }
            };

            session.BeginTransaction();
            await session.Save(testItem);
            await session.Commit();
            session.CloseTransaction();

            try
            {
                IWatchItemAccess itemAccess = new SqlWatchItemAccess(session);
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

        [TestMethod]
        public async Task CanUpdateLogsWithData()
        {
            var access = new SqlWatchItemAccess(session);
            var testItem = new WatchItem
            {
                WatchItemName = "test2",
                WebsiteUrl = "https://www.google.com",
                ItemPath = "/html",
                WatchItemLogs = new List<WatchItemLog>
                {
                    new WatchItemLog
                    {
                        LoggedAt = DateTime.Now,
                        Price = 12.55M
                    }
                }
            };

            session.BeginTransaction();

            // TODO: Review exception "object references an unsaved transient instance - save the transient instance before flushing or set cascade action for the property to something that would make it autosave. Type: WatchItemData.WatchItemLog, Entity: WatchItemData.WatchItemLog'"
            await session.Save(testItem);
            await session.Commit();
            session.CloseTransaction();

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
    }
}
