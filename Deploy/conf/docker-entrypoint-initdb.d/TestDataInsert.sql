USE ItemPriceWatcher;

INSERT INTO WatchItem (WatchItemName, WebsiteUrl, ItemPath)
VALUES
    ('Raspberry Pi Zero', 'https://www.adafruit.com/product/3708', '//span[@itemprop=''price'']');

INSERT INTO Contact (WatchItemID, FirstName, Surname, Email)
VALUES
    (1, 'Kevin', 'Burgon', 'test@test.com');

-- SELECT *
-- FROM
--     ItemPriceWatcher.WatchItem;