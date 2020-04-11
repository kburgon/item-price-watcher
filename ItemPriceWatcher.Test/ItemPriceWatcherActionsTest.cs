using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItemPriceWatcher.Test.Mocks;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

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
    }
}