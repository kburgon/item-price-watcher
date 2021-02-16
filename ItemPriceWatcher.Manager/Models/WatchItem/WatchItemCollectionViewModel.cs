using System.Collections.Generic;

namespace ItemPriceWatcher.Manager.Models.WatchItem
{
    public class WatchItemCollectionViewModel
    {
        public List<WatchItemViewModel> WatchItems { get; set; } = new List<WatchItemViewModel>();
    }
}