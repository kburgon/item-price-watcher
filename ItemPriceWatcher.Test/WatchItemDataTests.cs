using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using WatchItemData;
using WatchItemData.WatchItemAccess;
using ItemPriceWatcher.Test.Mocks;

namespace ItemPriceWatcher.Test
{
    [TestClass]
    public class WatchItemDataTests
    {
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
