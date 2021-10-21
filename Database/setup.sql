CREATE DATABASE pricewatcher;
\c pricewatcher;
CREATE SCHEMA pricewatcher;
CREATE TABLE test_table (id INT GENERATED ALWAYS AS IDENTITY, value VARCHAR NOT NULL);