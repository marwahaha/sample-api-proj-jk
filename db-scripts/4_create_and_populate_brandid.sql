ALTER TABLE dbo.Product ADD [BrandId] [smallint];
GO

UPDATE dbo.Product
SET dbo.Product.BrandId = dbo.Brand.BrandId
FROM dbo.Product 
INNER JOIN dbo.Brand
ON dbo.Product.Brand = dbo.Brand.BrandCode;
