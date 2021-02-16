using System.Collections.Generic;
using ItemPriceWatcher.Manager.BLL;
using ItemPriceWatcher.Manager.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using WatchItemData;

namespace ItemPriceWatcher.Manager.Tests
{
    [TestClass]
    public class WatchItemManagerShould
    {
        [TestMethod]
        public void GetWatchItems()
        {
            var watchItemRepo = Mock.Of<IWatchItemRepository>();
            var watchItem = new WatchItem
            {
                WatchItemID = 5,
                WatchItemName = "Toothbrush",
                WebsiteUrl = "https://www.google.com",
                ItemPath = "html"
            };

            Mock.Get(watchItemRepo)
                .Setup(mock => mock.GetAllWatchItems())
                .Returns(new List<WatchItem> { watchItem });

            WatchItemManager sut = new WatchItemManager(watchItemRepo);

            var result = sut.GetWatchItems();

            Assert.AreEqual(watchItem.WatchItemID, result.WatchItems[0].WatchItemId);
        }
    }
}
