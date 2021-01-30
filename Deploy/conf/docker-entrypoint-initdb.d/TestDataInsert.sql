USE ItemPriceWatcher;

INSERT INTO WatchItem (WatchItemName, WebsiteUrl, ItemPath)
VALUES
    ('Raspberry Pi Zero', 'https://www.adafruit.com/product/3708', '//span[@itemprop=''price'']');

-- SELECT *
-- FROM
--     ItemPriceWatcher.WatchItem;