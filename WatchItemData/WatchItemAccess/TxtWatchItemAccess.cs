using System;
using System.Collections.Generic;

namespace WatchItemData.WatchItemAccess
{
    class TxtWatchItemAccess : IWatchItemAccess
    {
        private string fullFilePath;
        
        /// <summary>
        /// Constructor that sets the file path from the given <paramref name="path">.
        /// </summary>
        /// <param name="path">The full file path of the file containing watch items</param>
        public TxtWatchItemAccess(string path)
        {
            fullFilePath = path;
        }

        public List<WatchItem> GetWatchItems()
        {
            throw new NotImplementedException();
        }
    }
}