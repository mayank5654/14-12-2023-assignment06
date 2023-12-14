CREATE DATABASE productinventorydb

USE productinventorydb

CREATE TABLE [dbo].[Products] (
    [ProductId] INT NOT NULL PRIMARY KEY IDENTITY (1, 1),
    [ProductName] NVARCHAR (50) NOT NULL,
    [Price] FLOAT NOT NULL,
    [Quantity] INT NOT NULL,
    [MfDate] DATE NOT NULL,
    [ExpDate] DATE NOT NULL
)