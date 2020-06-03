USE ItemPriceWatcher;

INSERT INTO WatchItems (itemName, websiteUrl, itemPath)
VALUES
    ('TestItem', 'https://www.google.com', 'html');

SELECT *
FROM
    ItemPriceWatcher.WatchItems;