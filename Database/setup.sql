CREATE DATABASE pricewatcher;
\c pricewatcher;
-- CREATE SCHEMA pricewatcher;
CREATE TABLE test_table (id INT GENERATED ALWAYS AS IDENTITY, value VARCHAR NOT NULL);
CREATE USER pricewatcheruser ENCRYPTED PASSWORD 'testpassword';
GRANT CONNECT ON DATABASE pricewatcher TO pricewatcheruser;
GRANT SELECT, INSERT ON ALL TABLES IN SCHEMA public TO pricewatcheruser;