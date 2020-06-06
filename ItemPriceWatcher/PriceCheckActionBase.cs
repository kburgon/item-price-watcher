using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using WatchItemData;

namespace ItemPriceWatcher
{
    public abstract class PriceCheckActionBase : IDisposable
    {
        private IWebDriver driver;
        private WatchItem watchItem;

        public PriceCheckActionBase(WatchItem watchItem)
        {
            driver = new FirefoxDriver();
            this.watchItem = watchItem;
        }

        public decimal GetItemPrice()
        {
            driver.Navigate().GoToUrl(watchItem.WebsiteUrl);
            return GetPriceFromPage();
        }

        protected abstract decimal GetPriceFromPage();

        public void Dispose()
        {
            driver.Close();
        }
    }
}