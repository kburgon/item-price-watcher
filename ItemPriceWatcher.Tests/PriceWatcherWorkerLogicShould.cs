using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ItemPriceWatcher.BusinessLogic;
using ItemPriceWatcher.Data.Access;
using ItemPriceWatcher.Data.Models;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace ItemPriceWatcher.Tests
{
    public class Tests
    {
        private PriceWatcherWorkerLogic _logic;
        private ILogger<PriceWatcherWorkerLogic> _loggerMock;
        private IWatchItemAccess _watchItemAccessMock;
        private IWatchItemLogAccess _watchItemLogAccessMock;
        private IContactAccess _contactAccessMock;
        private IPriceAccess _priceAccess;

        [SetUp]
        public void Setup()
        {
            _loggerMock = Mock.Of<ILogger<PriceWatcherWorkerLogic>>();
            _watchItemAccessMock = Mock.Of<IWatchItemAccess>();
            _watchItemLogAccessMock = Mock.Of<IWatchItemLogAccess>();
            _contactAccessMock = Mock.Of<IContactAccess>();
            _priceAccess = Mock.Of<IPriceAccess>();
            _logic = new PriceWatcherWorkerLogic(_loggerMock,
                                                 _watchItemAccessMock,
                                                 _watchItemLogAccessMock,
                                                 _contactAccessMock,
                                                 _priceAccess);
        }

        [Test]
        public async Task RunAsync_GetWatchItems()
        {
            SetupMockReturnsForBasicModelAccess();

            await _logic.RunAsync();

            Mock.Get(_watchItemAccessMock).Verify(m => m.GetAllWatchItems(), Times.Once);
        }

        [Test]
        public async Task RunAsync_GetMostRecentWatchItemLog()
        {
            SetupMockReturnsForBasicModelAccess();

            await _logic.RunAsync();

            Mock.Get(_watchItemLogAccessMock).Verify(m => m.GetMostRecentLogForWatchItemID(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public async Task RunAsync_GetPriceFromPriceScraper()
        {
            SetupMockReturnsForBasicModelAccess();

            await _logic.RunAsync();

            Mock.Get(_priceAccess).Verify(m => m.GetPriceAsync(It.IsAny<WatchItem>()), Times.Once);
        }

        [Test]
        public async Task RunAsync_GetContactForPriceDrop()
        {
            const decimal TEST_PRICE = 2.00M;
            SetupMockReturnsForBasicModelAccess(TEST_PRICE);

            await _logic.RunAsync();

            Mock.Get(_contactAccessMock).Verify(m => m.GetContactsForWatchItemId(It.IsAny<int>()), Times.Once);
        }

        private void SetupMockReturnsForBasicModelAccess(decimal fakePrice = 5.00M)
        {
            Mock.Get(_watchItemAccessMock).Setup(m => m.GetAllWatchItems()).Returns(GetMockWatchItems());
            Mock.Get(_watchItemLogAccessMock).Setup(m => m.GetMostRecentLogForWatchItemID(It.IsAny<int>())).Returns(GetMockWatchItemLog());
            Mock.Get(_priceAccess).Setup(m => m.GetPriceAsync(It.IsAny<WatchItem>())).ReturnsAsync(fakePrice);
        }

        private IEnumerable<WatchItem> GetMockWatchItems()
        {
            return new List<WatchItem>
            {
                new()
                {
                    WatchItemID = 5,
                    WatchItemName = "Test",
                    WebsiteUrl = "https://www.google.com",
                    ItemPath = "//html"
                }
            };
        }

        private WatchItemLog GetMockWatchItemLog()
        {
            return new()
            {
                WatchItemLogID = 7,
                Price = 5.00M,
                LoggedAt = new DateTime(2020, 5, 7)
            };
        }
    }
}