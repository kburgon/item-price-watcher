namespace ItemPriceWatcher.Manager.Models.WatchItem
{
    public class WatchItemViewModel
    {
        public int WatchItemId { get; set; }
        public int ContactId { get; set; }
        public string WatchItemName { get; set; }
        public string URL { get; set; }
        public string ItemPath { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
    }
}