using WatchItemData;

namespace ItemPriceWatcher.Test.Mocks
{
    internal class MockPriceCheckAction : PriceCheckActionBase
    {
        public MockPriceCheckAction(WatchItem watchItem)
            :base(watchItem)
        { }

        protected override decimal GetPriceFromPage()
        {
            return 0.00M;
        }
    }
}