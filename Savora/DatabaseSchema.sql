use savora;

CREATE TABLE users(
	Username VARCHAR(50),
	Password VARCHAR(50),
	Role VARCHAR(50)
);

ALTER TABLE users
ADD CONSTRAINT Username UNIQUE (Username);


CREATE TABLE category(
	ID INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50),
);

CREATE TABLE tables(
	ID INT PRIMARY KEY IDENTITY,
	Name VARCHAR(20),
);

CREATE TABLE staff (
	ID INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50),
	Phone VARCHAR(20),
	Role VARCHAR(50)
);

CREATE TABLE products(
	ID INT PRIMARY KEY IDENTITY,
	Name VARCHAR(50),
	Price FLOAT,
	CategoryID INT,
	Image IMAGE,
);

SELECT p.ID, p.Name, Price, CategoryID, c.Name As CatName FROM products p INNER JOIN category c ON c.ID = p.ID;


SELECT * FROM category;
SELECT * FROM users;
SELECT * FROM staff;
SELECT * FROM products;
SELECT p.ID, p.Name, Price, CategoryID, c.Name AS CatName 
FROM products p 
INNER JOIN category c ON c.ID = p.CategoryID;


