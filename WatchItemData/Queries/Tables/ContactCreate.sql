USE ItemPriceWatcher;

CREATE TABLE Contact (
    ContactID INT AUTO_INCREMENT,
    WatchItemID INT NOT NULL,
    FirstName VARCHAR(15) NOT NULL,
    Surname VARCHAR(30) NOT NULL,
    Email VARCHAR(40) NOT NULL,
    PRIMARY KEY (ContactID),
    FOREIGN KEY (WatchItemID) REFERENCES WatchItem(WatchItemID)
)