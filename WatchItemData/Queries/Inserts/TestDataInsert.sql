USE ItemPriceWatcher;

INSERT INTO WatchItem (WatchItemName, WebsiteUrl, ItemPath)
VALUES
    ('TestItem', 'https://www.google.com', 'html');

SELECT *
FROM
    ItemPriceWatcher.WatchItem;