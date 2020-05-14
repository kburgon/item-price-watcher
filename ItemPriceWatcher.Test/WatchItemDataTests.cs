using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WatchItemData;
using WatchItemData.WatchItemAccess;
using ItemPriceWatcher.Test.Mocks;
using NHibernate.Cfg;
using NHibernate.Driver;
using System.Reflection;

namespace ItemPriceWatcher.Test
{
    [TestClass]
    public class WatchItemDataTests
    {
        private Configuration configuration;

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
    }
}
