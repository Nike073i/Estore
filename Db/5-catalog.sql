CREATE TABLE IF NOT EXISTS Author (
	AuthorId SERIAL PRIMARY KEY,
	FirstName VARCHAR(50),
	MiddleName VARCHAR(50),
	LastName VARCHAR(50),
	Description TEXT,
	AuthorImage VARCHAR(100),
	UniqueId VARCHAR(50)
);

CREATE TABLE IF NOT EXISTS ProductSerie (
	ProductSerieId SERIAL PRIMARY KEY,
	SerieName VARCHAR (50)
);

CREATE TABLE IF NOT EXISTS Category (
	CategoryId SERIAL PRIMARY KEY,
	ParentCategoryId INT,
	CategoryName VARCHAR(50),
	CategoryUniqueId VARCHAR(50)
);

CREATE INDEX IF NOT EXISTS IX_Category_ParentCategoryId ON Category(ParentCategoryId);

CREATE TABLE IF NOT EXISTS Product (
	ProductId SERIAL PRIMARY KEY,
	CategoryId INT,
	ProductName VARCHAR(200),
	Price INT,
	Description TEXT,
	ProductImage VARCHAR(200),
	ReleaseDate TIMESTAMP,
	UniqueId VARCHAR(100),
	ProductSerieId INT
);

CREATE INDEX IF NOT EXISTS IX_Product_ProductCategoryId ON Product(CategoryId);
CREATE INDEX IF NOT EXISTS IX_Product_ProductSerieId ON Product(ProductSerieId);

CREATE TABLE IF NOT EXISTS ProductDetail (
	ProductDetailId SERIAL PRIMARY KEY,
	ProductId INT,
	ParamName VARCHAR(50),
	StringValue VARCHAR(100)
);

CREATE INDEX IF NOT EXISTS IX_ProductDetail_ProductId ON ProductDetail(ProductId);

CREATE TABLE IF NOT EXISTS ProductAuthor (
	ProductId INT,
	AuthorId INT,
	PRIMARY KEY (ProductId, AuthorId)
);