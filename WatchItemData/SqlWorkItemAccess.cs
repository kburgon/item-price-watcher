using System;
using System.Collections.Generic;
using SharedObjects;

namespace WatchItemData
{
    public class SqlWorkItemAccess : IWorkItemAccess
    {
        public List<WatchItem> GetActiveWatchItems()
        {
            return new List<WatchItem>();
        }
    }
}