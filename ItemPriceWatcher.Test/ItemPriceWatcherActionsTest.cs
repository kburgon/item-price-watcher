using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemPriceWatcher.Test.Mocks;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using WatchItemData;

namespace ItemPriceWatcher.Test
{
    [TestClass]
    public class ItemPriceWatcherActionsTest
    {
        /// <summary>
        /// The Basic Selenium Webdriver can be opened.
        /// </summary>
        [TestMethod]
        public void CanOpenWebDriver()
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Close();
        }

        /// <summary>
        /// The Item Price Checker action class can open the driver.
        /// </summary>
        [TestMethod]
        public void CanOpenDriverFromPriceCheckerAction()
        {
            WatchItem watchItem = new WatchItem
            {
                WatchItemID = 0,
                WatchItemName = "TestItem",
                WebsiteUrl = @"https://www.google.com",
                ItemPath = @"html"
            };

            using (PriceCheckActionBase testPriceCheckAction = new MockPriceCheckAction(watchItem))
            {
                Assert.AreEqual(0.00M, testPriceCheckAction.GetItemPrice());
            }
        }
    }
}