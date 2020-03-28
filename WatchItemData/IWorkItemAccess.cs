using System;
using System.Collections.Generic;
using SharedObjects;

namespace WatchItemData
{
    public interface IWorkItemAccess
    {
        List<WatchItem> GetActiveWatchItems();
    }
}
