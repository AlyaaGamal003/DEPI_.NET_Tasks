-- Practice
Select  * FROM  HumanResources.Employee
Select  * FROM  HumanResources.EmployeeDepartmentHistory
Select  * FROM  HumanResources.EmployeePayHistory
Select  * FROM  HumanResources.vEmployee  -- view
Select  * FROM  Production.Product
Select  * FROM  Sales.Customer
Select  * FROM  Person.BusinessEntityAddress
Select  * FROM  Person.Person
Select  * FROM  Person.Address
Select  * FROM  Production.ProductCategory 
Select  * FROM  Production.ProductSubcategory  
SELECT  * FROM  Sales.SalesOrderDetail
SELECT  * FROM  Sales.SalesOrderHeader 
SELECT  * FROM  Sales.SpecialOfferProduct
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 1.1 List all employees hired after January 1, 2012, showing their ID, first name, last name, and hire date, ordered by hire date descending.
Select p.BusinessEntityID ,p.FirstName + ' ' + p.MiddleName +' ' + p.LastName As Full_Name, e.HireDate
From Person.Person p Join
HumanResources.Employee e on
p.BusinessEntityID = e.BusinessEntityID
WHERE e.HireDate > '2012-01-01'
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--1.2 List products with a list price between $100 and $500, showing product ID, name, list price, and product number, ordered by list price ascending.

Select  ProductID, Name,ProductNumber, ListPrice
FROM  Production.Product
WHERE ListPrice between 100 AND 500
Order By ListPrice DESC
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 1.3 List customers from the cities 'Seattle' or 'Portland', showing customer ID, first name, last name, and city, using appropriate joins.
Select  * FROM  Sales.Customer -- customer_id
Select  * FROM  Person.BusinessEntityAddress
Select  * FROM  Person.Person
Select  * FROM  Person.Address --city

SELECT 
    c.CustomerID,
    p.FirstName + ' ' +p.LastName AS Full_Name,
    a.City
FROM Sales.Customer c
JOIN Person.Person p 
    ON c.PersonID = p.BusinessEntityID
JOIN Person.BusinessEntityAddress bea 
    ON c.PersonID = bea.BusinessEntityID
JOIN Person.Address a 
    ON bea.AddressID = a.AddressID
WHERE a.City IN ('Seattle', 'Portland');

-------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 1.4 List the top 15 most expensive products currently being sold, showing name, list price, product number, and category name, excluding discontinued products.
SELECT TOP (15) 
    pc.Name AS Category_Name,
    p.Name AS Product_Name,
    p.ProductNumber,
    p.ListPrice
FROM Production.Product p
JOIN Production.ProductSubcategory ps 
    ON p.ProductSubcategoryID = ps.ProductSubcategoryID
JOIN Production.ProductCategory pc 
    ON ps.ProductCategoryID = pc.ProductCategoryID
WHERE p.SellEndDate IS NULL  -- Exclude discontinued products
ORDER BY p.ListPrice DESC;
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 2.1 List products whose name contains 'Mountain' and color is 'Black', showing product ID, name, color, and list price.
SELECT 
      ProductID,
	  Name AS Product_Name,
	  Color,
	  ListPrice
FROM Production.Product
WHERE Name LIKE '%Mountain%' AND color = 'Black';
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 2.2 List employees born between January 1, 1970, and December 31, 1985, showing full name, birth date, and age in years.
SELECT 
    p.FirstName + ' ' + ISNULL(p.MiddleName, '') + ' ' + p.LastName AS Employee_Name,
    e.BirthDate,
    DATEDIFF(YEAR, e.BirthDate, GETDATE()) AS Age
FROM HumanResources.Employee e
JOIN Person.Person p ON e.BusinessEntityID = p.BusinessEntityID
WHERE e.BirthDate BETWEEN '1970-01-01' AND '1985-12-31';
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 2.3 List orders placed in the fourth quarter of 2013, showing order ID, order date, customer ID, and total due.
-- fourth quarter of 2013 --> October 1, 2013 → December 31, 2013
SELECT 
      SalesOrderID,
	  OrderDate,
	  CustomerID,
	  TotalDue
FROM Sales.SalesOrderHeader  
Where OrderDate BETWEEN '2013-10-01' AND '2013-12-31'
ORDER BY OrderDate
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 2.4 List products with a null weight but a non-null size, showing product ID, name, weight, size, and product number.
SELECT 
      ProductID,
	  Name AS Product_Name,
	  Weight,
	  Size,
	  ProductNumber
FROM Production.Product
WHERE Weight IS NULL AND Size IS NOT NULL
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 3.1 Count the number of products by category, ordered by count descending.
SELECT
      pc.Name AS CategoryName,
      COUNT(p.ProductID) AS Total_Products
FROM Production.Product p 
JOIN Production.ProductSubcategory psc
ON p.ProductSubcategoryID = psc.ProductSubcategoryID
JOIN Production.ProductCategory pc
ON psc.ProductCategoryID = pc.ProductCategoryID
GROUP BY pc.Name
ORDER BY COUNT(p.ProductID) DESC
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 3.2 Show the average list price by product subcategory, including only subcategories with more than five products.
SELECT 
       psc.Name AS Sub_Category_Name,
	   AVG(p.ListPrice) AS Average_Pice
FROM Production.Product p 
JOIN Production.ProductSubcategory psc
ON p.ProductSubcategoryID = psc.ProductSubcategoryID
WHERE p.ListPrice > 0
GROUP BY psc.Name
HAVING COUNT(p.ProductID) > 5
ORDER BY Average_Pice DESC;
------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 3.2 Show the average list price by product subcategory, including only subcategories with more than five products.
-- 3.3 List the top 10 customers by total order count, including customer name.
SELECT TOP(10) 
    p.FirstName + ' ' +p.LastName AS Full_Name,
	COUNT(soh.SalesOrderID) AS Total_Orders
FROM Sales.SalesOrderHeader soh
JOIN Sales.Customer c 
ON  soh.CustomerID = c.CustomerID
JOIN Person.Person p 
    ON c.PersonID = p.BusinessEntityID
GROUP BY p.FirstName , p.LastName
ORDER BY Total_Orders DESC;
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 3.4 Show monthly sales totals for 2013, displaying the month name and total amount.
SELECT  
      DATENAME(MONTH, OrderDate) AS MonthName,
      SUM(TotalDue) AS TotalSales
FROM  Sales.SalesOrderHeader 
WHERE YEAR(OrderDate)= 2013 
GROUP BY MONTH(OrderDate), DATENAME(MONTH, OrderDate)
ORDER BY MONTH(OrderDate);
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 4.1 Find all products launched in the same year as 'Mountain-100 Black, 42'. Show product ID, name, sell start date, and year.

SELECT
      ProductID,
	  Name AS Product_Name,
	  SellStartDate,
	  YEAR(SellStartDate) AS Year
FROM Production.Product
WHERE YEAR(SellStartDate) = 
            ( SELECT YEAR(SellStartDate) 
			  FROM Production.Product
              WHERE Name = 'Mountain-100 Black, 42')
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 4.2 Find employees who were hired on the same date as someone else. Show employee names, shared hire date,
-- and the count of employees hired that day.
-- Select  * FROM  HumanResources.Employee   --  hire date
-- Select  * FROM  Person.Person  -- name
Go
WITH HireGroups AS (
    SELECT 
        HireDate,
        COUNT(*) AS Employees_Count
    FROM HumanResources.Employee
    GROUP BY HireDate
    HAVING COUNT(*) > 1
)
SELECT 
    p.FirstName + ' ' + ISNULL(p.MiddleName, '') + ' ' + p.LastName AS Employee_Name,
    e.HireDate,
    hg.Employees_Count
FROM HumanResources.Employee e
JOIN HireGroups hg ON e.HireDate = hg.HireDate
JOIN Person.Person p ON e.BusinessEntityID = p.BusinessEntityID
ORDER BY e.HireDate;
go
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 5.1 Create a table named Sales.ProductReviews with columns for review ID, product ID, customer ID, rating, review date, review text, verified purchase flag,
-- and helpful votes.Include appropriate primary key, foreign keys, check constraints, defaults, and a unique constraint on product ID and customer ID.
-- review ID, product ID, customer ID, rating, review date, review text , verified purchase flag  & helpful votes
-- Select  * FROM  Sales.Customer -- customer ID
CREATE TABLE Sales.ProductReviews(
      reviewID INT IDENTITY(1,1) PRIMARY KEY,
	  productID  INT NOT NULL,
	  customerID  INT NOT NULL,
	  rating DECIMAL(10,2),
	  reviewDate DATE DEFAULT GETDATE() ,
	  reviewText VARCHAR(255),
	  verifiedPurchaseFlag BIT DEFAULT 0,
	  helpfulVotes INT ,
	  FOREIGN KEY (customerID) REFERENCES Sales.Customer(customerID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	  FOREIGN KEY (productID) REFERENCES Production.Product(productID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
	
	CONSTRAINT UQ_Product_Customer UNIQUE (ProductID, CustomerID)
);
SELECT * FROM Sales.ProductReviews

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 6.1 Add a column named LastModifiedDate to the Production.Product table, with a default value of the current date and time.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 6.2 Create a non-clustered index on the LastName column of the Person.Person table, including FirstName and MiddleName.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 6.3 Add a check constraint to the Production.Product table to ensure that ListPrice is greater than StandardCost.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 7.1 Insert three sample records into Sales.ProductReviews using existing product and customer IDs, with varied ratings and meaningful review text.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 7.2 Insert a new product category named 'Electronics' and a corresponding product subcategory named 'Smartphones' under Electronics.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 7.3 Copy all discontinued products (where SellEndDate is not null) into a new table named Sales.DiscontinuedProducts.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 8.1 Update the ModifiedDate to the current date for all products where ListPrice is greater than $1000 and SellEndDate is null.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 8.2 Increase the ListPrice by 15% for all products in the 'Bikes' category and update the ModifiedDate.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 8.3 Update the JobTitle to 'Senior' plus the existing job title for employees hired before January 1, 2010.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 9.1 Delete all product reviews with a rating of 1 and helpful votes equal to 0.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 9.2 Delete products that have never been ordered, using a NOT EXISTS condition with Sales.SalesOrderDetail.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 9.3 Delete all purchase orders from vendors that are no longer active.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 10.1 Calculate the total sales amount by year from 2011 to 2014, showing year, total sales, average order value, and order count.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 10.2 For each customer, show customer ID, total orders, total amount, average order value, first order date, and last order date.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 10.3 List the top 20 products by total sales amount, including product name, category, total quantity sold, and total revenue.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--10.4 Show sales amount by month for 2013, displaying the month name, sales amount, and percentage of the yearly total.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 11.1 Show employees with their full name, age in years, years of service, hire date formatted as 'Mon DD, YYYY', and birth month name.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 11.2 Format customer names as 'LAST, First M.' (with middle initial), extract the email domain, and apply proper case formatting.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 11.3 For each product, show name, weight rounded to one decimal, weight in pounds (converted from grams), and price per pound.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 12.1 List product name, category, subcategory, and vendor name for products that have been purchased from vendors.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 12.2 Show order details including order ID, customer name, salesperson name, territory name, product name, quantity, and line total.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 12.3 List employees with their sales territories, including employee name, job title, territory name, territory group, and sales year-to-date.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 13.1 List all products with their total sales, including those never sold. Show product name, category, total quantity sold (zero if never sold), and total revenue (zero if never sold).
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 13.2 Show all sales territories with their assigned employees, including unassigned territories. Show territory name, employee name (null if unassigned), and sales year-to-date.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 13.3 Show the relationship between vendors and product categories, including vendors with no products and categories with no vendors.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 14.1 List products with above-average list price, showing product ID, name, list price, and price difference from the average.
 ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 14.2 List customers who bought products from the 'Mountain' category, showing customer name, total orders, and total amount spent.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 14.3 List products that have been ordered by more than 100 different customers, showing product name, category, and unique customer count.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 14.4 For each customer, show their order count and their rank among all customers.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 15.1 Create a view named vw_ProductCatalog with product ID, name, product number, category, subcategory, list price, standard cost, profit margin percentage, inventory level, and status (active/discontinued).
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 15.2 Create a view named vw_SalesAnalysis with year, month, territory, total sales, order count, average order value, and top product name.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 15.3 Create a view named vw_EmployeeDirectory with full name, job title, department, manager name, hire date, years of service, email, and phone.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 15.4 Write three different queries using the views you created, demonstrating practical business scenarios.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 16.1 Classify products by price as 'Premium' (greater than $500), 'Standard' ($100 to $500), or 'Budget' (less than $100), and show the count and average price for each category.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 16.2 Classify employees by years of service as 'Veteran' (10+ years), 'Experienced' (5-10 years), 'Regular' (2-5 years), or 'New' (less than 2 years), and show salary statistics for each group.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 16.3 Classify orders by size as 'Large' (greater than $5000), 'Medium' ($1000 to $5000), or 'Small' (less than $1000), and show the percentage distribution.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 17.1 Show products with name, weight (display 'Not Specified' if null), size (display 'Standard' if null), and color (display 'Natural' if null).
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 17.2 For each customer, display the best available contact method, prioritizing email address, then phone, then address line.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 17.3 Find products where weight is null but size is not null, and also find products where both weight and size are null. Discuss the impact on inventory management.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 18.1 Create a recursive query to show the complete employee hierarchy, including employee name, manager name, hierarchy level, and path.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 18.2 Create a query to compare year-over-year sales for each product, showing product, sales for 2013, sales for 2014, growth percentage, and growth category.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 19.1 Rank products by sales within each category, showing product name, category, sales amount, rank, dense rank, and row number.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 19.2 Show the running total of sales by month for 2013, displaying month, monthly sales, running total, and percentage of year-to-date.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 19.3 Show the three-month moving average of sales for each territory, displaying territory, month, sales, and moving average.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 19.4 Show month-over-month sales growth, displaying month, sales, previous month sales, growth amount, and growth percentage.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 19.5 Divide customers into four quartiles based on total purchase amount, showing customer name, total purchases, quartile, and quartile average.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 20.1 Create a pivot table showing product categories as rows and years (2011-2014) as columns, displaying sales amounts with totals.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 20.2 Create a pivot table showing departments as rows and gender as columns, displaying employee count by department and gender.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 20.3 Create a dynamic pivot table for quarterly sales, automatically handling an unknown number of quarters.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 21.1 Find products sold in both 2013 and 2014, and combine with products sold only in 2013, showing a complete analysis.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 21.2 Compare product categories with high-value products (greater than $1000) to those with high-volume sales (more than 1000 units sold), using set operations.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 22.1 Declare variables for the current year, total sales, and average order value, and display year-to-date statistics with formatted output.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 22.2 Check if a specific product exists in inventory. If it exists, show details; if not, suggest similar products.     22.3 Generate a monthly sales summary for each month in 2013 using a loop.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 22.4 Implement error handling for a product price update operation, including logging errors and rolling back on failure.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 23.1 Create a scalar function to calculate customer lifetime value, including total amount spent and weighted recent activity, with parameters for date range and activity weight.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 23.2 Create a multi-statement table-valued function to return products by price range and category, including error handling for invalid parameters.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 23.3 Create an inline table-valued function to return all employees under a specific manager, including hierarchy level and employee path.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 24.1 Create a stored procedure to get products by category, with parameters for category name, minimum price, and maximum price, including parameter validation and error handling.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 24.2 Create a stored procedure to update product pricing, including an audit trail, business rule validation, and transaction management.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 24.3 Create a stored procedure to generate a comprehensive sales report for a given date range and territory, including summary statistics and detailed breakdowns.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 24.4 Create a stored procedure to process bulk orders from XML input, including transaction management, validation, error handling, and returning order confirmation details.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 24.5 Create a stored procedure to perform flexible product searches with dynamic filtering by name, category, price range, and date range, returning paginated results and total count.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 25.1 Create a trigger on Sales.SalesOrderDetail to update product inventory and maintain sales statistics after insert, including error handling and transaction management.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 25.2 Create a view combining multiple tables and implement an INSTEAD OF trigger for insert operations, handling complex business logic and data distribution.
-- ----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 25.3 Create an audit trigger for Production.Product price changes, logging old and new values with timestamp and user information.
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- 26.1 Create a filtered index for active products only (SellEndDate IS NULL) and for recent orders (last 2 years), and measure performance impact.

