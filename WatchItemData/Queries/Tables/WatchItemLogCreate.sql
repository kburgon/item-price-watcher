USE ItemPriceWatcher;

CREATE TABLE WatchItemLog (
    WatchItemLogID INT AUTO_INCREMENT,
    LoggedAt DATETIME NOT NULL,
    Price DECIMAL(6,2) NOT NULL,
    WatchItemID INT NOT NULL,
    PRIMARY KEY (WatchItemLogID),
    FOREIGN KEY (WatchItemID) REFERENCES WatchItem(WatchItemID)
)