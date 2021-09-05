using System;
using OpenQA.Selenium;
using WatchItemData;

namespace ItemPriceWatcher
{
    public class PriceCheckAction : PriceCheckActionBase
    {
        public PriceCheckAction(WatchItem watchItem) 
            : base(watchItem)
        { }

        protected override decimal GetPriceFromPage()
        {
            var priceText = driver.FindElement(By.XPath(watchItem.ItemPath)).Text;
            priceText = priceText.Replace("$", "").Replace(",", "").Trim();
            return Convert.ToDecimal(priceText);
        }
    }
}