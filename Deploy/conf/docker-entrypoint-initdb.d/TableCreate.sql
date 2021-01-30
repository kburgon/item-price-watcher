USE ItemPriceWatcher;

CREATE TABLE WatchItem (
    WatchItemID INT AUTO_INCREMENT PRIMARY KEY,
    WatchItemName VARCHAR(30) NOT NULL DEFAULT '',
    WebsiteUrl VARCHAR(200) NOT NULL DEFAULT '',
    ItemPath VARCHAR(200) NOT NULL DEFAULT ''
);

CREATE TABLE WatchItemLog (
    WatchItemLogID INT AUTO_INCREMENT,
    LoggedAt DATETIME NOT NULL,
    Price DECIMAL(6,2) NOT NULL,
    WatchItemID INT NOT NULL,
    PRIMARY KEY (WatchItemLogID),
    FOREIGN KEY (WatchItemID) REFERENCES WatchItem(WatchItemID)
);

CREATE TABLE Contact (
    ContactID INT AUTO_INCREMENT,
    WatchItemID INT NOT NULL,
    FirstName VARCHAR(15) NOT NULL,
    Surname VARCHAR(30) NOT NULL,
    Email VARCHAR(40) NOT NULL,
    PRIMARY KEY (ContactID),
    FOREIGN KEY (WatchItemID) REFERENCES WatchItem(WatchItemID)
);