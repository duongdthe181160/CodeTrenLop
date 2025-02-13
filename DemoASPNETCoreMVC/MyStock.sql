CREATE DATABASE MyStock;
USE MyStock;

CREATE TABLE Products (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    ProductName NVARCHAR(40) NOT NULL,
    UnitPrice MONEY NOT NULL,
    UnitsInStock INT NOT NULL
);

INSERT INTO Products (ProductName, UnitPrice, UnitsInStock)
VALUES 
    ('Genen Shouyu', 50.00, 39),
    ('Alice Mutton', 30.00, 17),
    ('Aniseed Syrup', 40.00, 13),
    ('Perth Pasties', 22.00, 53),
    ('Carnarvon Tigers', 21.35, 0),
    ('Gula Malacca', 25.00, 120),
    ('Steeleye Stout', 30.00, 15),
    ('Chocolade', 40.00, 6),
    ('Mishi Kobe Niku', 97.00, 29),
    ('Ikura', 31.00, 31);
