-- Drop tables in correct order to maintain FK constraints
IF OBJECT_ID('OrderItems', 'U') IS NOT NULL DROP TABLE OrderItems;
IF OBJECT_ID('Orders', 'U') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
IF OBJECT_ID('Customers', 'U') IS NOT NULL DROP TABLE Customers;

-- Create Products Table
CREATE TABLE Products (
    ProductId INT PRIMARY KEY IDENTITY,
    ProductName NVARCHAR(100) NOT NULL,
    ProductDesc NVARCHAR(255),
    Price DECIMAL(10, 2) NOT NULL,
    IsPhysical BIT NOT NULL,
	IsActive BIT NOT NULL DEFAULT(1),
	CreatedDate DateTime NOT NULL DEFAULT(GETDATE())
);

-- Create Customers Table
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY IDENTITY,
    CustomerName NVARCHAR(100),
    EmailAddress NVARCHAR(100),
    CustomerAddress NVARCHAR(255),
	IsActive BIT NOT NULL DEFAULT(1),
	CreatedDate DateTime NOT NULL DEFAULT(GETDATE())
);

-- Create Orders Table
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY,
    CustomerId INT NOT NULL,
    OrderDate DATETIME NOT NULL DEFAULT GETDATE(),
    TotalAmount DECIMAL(10, 2) NOT NULL,
	ShippingAddress NVARCHAR(2048)
);

-- Create OrderItems Table
CREATE TABLE OrderItems (
    OrderItemId INT PRIMARY KEY IDENTITY,
    OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(OrderId),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(ProductId),
    Quantity INT NOT NULL,
	--UnitPrice DECIMAL(10, 2) NOT NULL,
    DiscountedPrice DECIMAL(10, 2) NOT NULL,
	CreatedDate DateTime NOT NULL DEFAULT(GETDATE())
);

-- Update Orders table to FK Customers
IF COL_LENGTH('Orders', 'CustomerId') IS NOT NULL
BEGIN
    ALTER TABLE Orders
    ADD CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId);
END

-- Seed Products Table
INSERT INTO Products (ProductName, ProductDesc, Price, IsPhysical) VALUES 
('Milk', 'The best of cows', 1.50, 1),
('Bread', 'Easy toast', 1.00, 1),
('Eggs', 'Wild chicken', 2.00, 1),
('Butter', 'Creamy delight', 2.50, 1),
('Cheese', 'Sharp cheddar', 3.25, 1),
('Apples', 'Fresh and juicy', 2.75, 1),
('Banana', 'Roast Banana', 5.45, 1),
('Orange', 'Nagpuri Orange', 15.34, 1),
('Pen', 'Black Pen', 25.00, 1),
('EasyGroceries loyalty membership', 'Enjoy 20% discount on all future orders', 5.00, 0);

-- Seed a Customer
INSERT INTO Customers (CustomerName, EmailAddress, CustomerAddress) VALUES
('Shubh Laxmi', 'shubhlaxmi@example.com', '2B2 IMG, Whitefield, India'),
('Keshav Anand', 'keshav@example.com', '2B2 IMG, Whitefield, India'),
('Kaushiki Anand', 'kaushiki@example.com', '2B2 IMG, Whitefield, India'),
('Ram Vinay Kumar', 'rvk@example.com', 'Channasandra, Whitefield, India'),
('Amit Singh', 'amit@example.com', 'Patna, India'),
('Richa Singh', 'richa@example.com', 'Patna City Road, Patna, India'),
('Tanya Prashant', 'tanya@example.com', 'Boring Road, Patna, India'),
('Prashant Ranjan', 'prashant@example.com', 'Housing Colony Road, Patna, India'),
('Avinash Verma', 'avinash@example.com', 'Dala Road, Bhagalpur, India'),
('John Doe', 'john@example.com', '123 London Road, UK');