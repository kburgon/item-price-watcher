USE ItemPriceWatcher;

CREATE TABLE WatchItem (
    WatchItemID INT AUTO_INCREMENT PRIMARY KEY,
    WatchItemName VARCHAR(30) NOT NULL DEFAULT '',
    WebsiteUrl VARCHAR(200) NOT NULL DEFAULT '',
    ItemPath VARCHAR(200) NOT NULL DEFAULT ''
);