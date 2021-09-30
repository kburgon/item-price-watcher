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

        [SetUp]
        public void Setup()
        {
            _loggerMock = Mock.Of<ILogger<PriceWatcherWorkerLogic>>();
            _watchItemAccessMock = Mock.Of<IWatchItemAccess>();
            _watchItemLogAccessMock = Mock.Of<IWatchItemLogAccess>();
            _contactAccessMock = Mock.Of<IContactAccess>();
            _logic = new PriceWatcherWorkerLogic(_loggerMock,
                                                 _watchItemAccessMock,
                                                 _watchItemLogAccessMock,
                                                 _contactAccessMock);
        }

        [Test]
        public async Task RunAsync_GetsWatchItems()
        {
            SetupMockReturnsForBasicModelAccess();

            await _logic.RunAsync();

            Mock.Get(_watchItemAccessMock).Verify(m => m.GetAllWatchItems(), Times.Once);
        }

        [Test]
        public async Task RunAsync_GetsMostRecentWatchItemLog()
        {
            SetupMockReturnsForBasicModelAccess();

            await _logic.RunAsync();

            Mock.Get(_watchItemLogAccessMock).Verify(m => m.GetMostRecentLogForWatchItemID(It.IsAny<int>()), Times.Once);
        }

        private void SetupMockReturnsForBasicModelAccess()
        {
            Mock.Get(_watchItemAccessMock).Setup(m => m.GetAllWatchItems()).Returns(GetMockWatchItems());
            Mock.Get(_watchItemLogAccessMock).Setup(m => m.GetMostRecentLogForWatchItemID(It.IsAny<int>())).Returns(GetMockWatchItemLog());
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