using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using System;
using WatchItemData;

namespace ItemPriceWatcher
{
    public abstract class PriceCheckActionBase : IDisposable
    {
        protected IWebDriver driver;
        protected WatchItem watchItem;

        public PriceCheckActionBase(WatchItem watchItem)
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AddArgument("--headless");
            driver = new FirefoxDriver(options);
            // driver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), new FirefoxOptions());
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