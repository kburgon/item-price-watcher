USE ItemPriceWatcher;

CREATE TABLE WatchItems (
    id INT AUTO_INCREMENT PRIMARY KEY,
    itemName VARCHAR(30) NOT NULL,
    websiteUrl VARCHAR(200) NOT NULL,
    itemPath VARCHAR(200) NOT NULL
);