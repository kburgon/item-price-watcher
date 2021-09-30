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
            Mock.Get(_watchItemAccessMock).Setup(m => m.GetAllWatchItems()).Returns(GetMockWatchItems());
            await _logic.RunAsync();
            Mock.Get(_watchItemAccessMock).Verify(m => m.GetAllWatchItems(), Times.Once);
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
    }
}