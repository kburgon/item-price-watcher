using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WatchItemData;
using WatchItemData.ORM;

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
            await session.SafeSaveAsync(testItem);

            try
            {
                List<WatchItem> watchItems = session.Objects.ToList();
                Assert.IsTrue(watchItems.Count > 0);
            }
            finally
            {
                await session.SafeDeleteAsync(testItem);
            }
        }

        /// <summary>
        /// Passes if a test item can be saved and retrieved from the database with logs.
        /// </summary>
        [TestMethod]
        public async Task CanRetrieveLogsWithData()
        {
            var testItem = CreateTestItem();
            await session.SafeSaveAsync(testItem);

            try
            {
                List<WatchItem> watchItems = session.Objects.ToList();
                Assert.AreEqual(testItem.WatchItemLogs.Last().Price, watchItems.Last().WatchItemLogs.Last().Price);
            }
            finally
            {
                await session.SafeDeleteAsync(testItem);
            }
        }

        /// <summary>
        /// Passes if a WatchItem object can add a new log and it persists to the database.
        /// </summary>
        [TestMethod]
        public async Task CanUpdateLogsWithData()
        {
            var testItem = CreateTestItem();
            await session.SafeSaveAsync(testItem);

            try
            {
                testItem.AddLog(new WatchItemLog
                {
                    LoggedAt = DateTime.Now,
                    Price = 12.34M
                });

                await session.SafeSaveAsync(testItem);
                var results = session.Objects.ToList();
                Assert.IsTrue(results.Last().WatchItemLogs.Last().Price == testItem.WatchItemLogs.Last().Price);
            }
            finally
            {
                await session.SafeDeleteAsync(testItem);
            }
        }

        /// <summary>
        /// Test passes if a contact object can be added to a WatchItem and saved to the DB.
        /// </summary>
        [TestMethod]
        public async Task CanPersistContact()
        {
            var testItem = CreateTestItem();
            
            try
            {
                await session.SafeSaveAsync(testItem);
                var results = session.Objects.ToList();
                Assert.IsTrue(results.Last().Contacts.Count == 1);
            }
            finally
            {
                await session.SafeDeleteAsync(testItem);
            }
        }

        [TestMethod]
        public async Task CanAddMultipleContacts()
        {
            var testItem = CreateTestItem();

            try
            {
                await session.SafeSaveAsync(testItem);
                var contact = new Contact
                {
                    FirstName = "Fred",
                    Surname = "Mercury",
                    Email = "fmTest@queen.com"
                };

                testItem.AddContact(contact);
                await session.SafeSaveAsync(testItem);
                
                var results = session.Objects.ToList();
                Assert.IsTrue(results.Last().Contacts.Last().Email == contact.Email);
            }
            finally
            {
                await session.SafeDeleteAsync(testItem);
            }
        }

        /// <summary>
        /// Tests to ensure that all collections can be visited.  This was created after loading a collection threw exceptions.
        /// </summary>
        [TestMethod]
        public void CanIterateThroughWatchItems()
        {
            var watchItems = session.Objects.ToArray();
            foreach (var item in watchItems)
            {
                foreach (var log in item.WatchItemLogs)
                {
                    // Visit each element in the list
                }

                foreach (var contact in item.Contacts)
                {
                    // Visit each element in the list
                }
            }

            // No exceptions were thrown.  Test passed!
        }

        private WatchItem CreateTestItem()
        {
            var item = new WatchItem
            {
                WatchItemName = "test",
                WebsiteUrl = "https://www.google.com",
                ItemPath = "/html",
                WatchItemLogs = new List<WatchItemLog> { },
                Contacts = new HashSet<Contact> { }
            };

            var log = new WatchItemLog
            {
                LoggedAt = DateTime.Now,
                Price = 12.55M
            };

            var contact = new Contact
            {
                FirstName = "Kevin",
                Surname = "BurgonInt",
                Email = "kburgintegrations@gmail.com"
            };

            item.AddLog(log);
            item.AddContact(contact);
            return item;
        }
    }
}
