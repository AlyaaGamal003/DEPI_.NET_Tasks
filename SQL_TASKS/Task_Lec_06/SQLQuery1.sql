--1. Customer Spending Analysis#
--Write a query that uses variables to find the total amount spent by customer ID 1.
--Display a message showing whether they are a VIP customer (spent > $5000) or regular customer.

DECLARE @customer_id INT = 1;
DECLARE @total_spent DECIMAL(10,2);
DECLARE @customer_status VARCHAR(50);

SELECT @total_spent = SUM(oi.quantity * oi.list_price * (1 - oi.discount))
FROM sales.orders o
JOIN sales.order_items oi ON o.order_id = oi.order_id
WHERE o.customer_id = @customer_id;

SET @customer_status = 
    CASE 
        WHEN @total_spent > 5000 THEN 'VIP Customer'
        ELSE 'Regular Customer'
    END;

SELECT 
    @customer_id AS CustomerID,
    @total_spent AS TotalSpent,
    @customer_status AS CustomerStatus;

------------------------------------------------------------------------------------------------------------------------------------

--2. Product Price Threshold Report#
--Create a query using variables to count how many products cost more than $1500. 
--Store the threshold price in a variable and display both the threshold and count in a formatted message.

DECLARE @total_Products int = 0;
DECLARE @threshold_price DECIMAL(10, 2) = 1500;

SELECT @total_Products = count(product_id)   from production.products Where list_price > 1500;
print'There are '+CAST( @total_products AS VARCHAR(25) ) + ' products with price above $'+ CAST(@threshold_price AS VARCHAR(25))
SELECT 
    @threshold_price AS Threshold_Price,
    @total_products AS Products_Above_Threshold,
    CONCAT('There are ', @total_products, ' products with price above $', @threshold_price) AS Report_Message;

------------------------------------------------------------------------------------------------------------------------------------
--3. Staff Performance Calculator#
--Write a query that calculates the total sales for staff member ID 2 in the year 2017. 
--Use variables to store the staff ID, year, and calculated total. Display the results with appropriate labels.

DECLARE @Staff_ID INT = 2;
DECLARE @Year INT =2017;
DECLARE @Total_Sales Decimal(20,2) = 0;

SELECT  @Total_Sales = SUM(oi.list_price * oi.quantity * (1 - oi.discount))   FROM  sales.order_items oi 
JOIN sales.orders o ON oi.order_id =o.order_id WHERE o.staff_id= @Staff_ID AND YEAR(o.order_date) =@Year;

SELECT
     @Staff_ID AS Staff_ID, 
	 @Year AS Target_Year,
	 ISNULL(@Total_Sales, 0) AS Total_Sales;

PRINT 'Total sales for staff member ID ' + CAST(@Staff_ID AS VARCHAR(255)) + 
      ' in ' + CAST(@Year AS VARCHAR(255)) + 
      ' is $' + CAST(ISNULL(@Total_Sales, 0) AS VARCHAR (255)) 

------------------------------------------------------------------------------------------------------------------------------------
-- 4. Global Variables Information#
-- Create a query that displays the current server name, SQL Server version, 
--and the number of rows affected by the last statement. Use appropriate global variables.
SELECT * FROM production.products WHERE list_price > 1000;
SELECT
@@SERVERNAME AS server_name,
@@VERSION AS sql_version,
@@ROWCOUNT AS last_rowcount

------------------------------------------------------------------------------------------------------------------------------------
-- 5.Write a query that checks the inventory level for product ID 1 in store ID 1. 
--Use IF statements to display different messages based on stock levels:#
--If quantity > 20: Well stocked
--If quantity 10-20: Moderate stock
--If quantity < 10: Low stock - reorder needed

DECLARE @Quan INT =0
SELECT @Quan = quantity
FROM production.stocks
where product_id = 1  AND store_id = 1

IF @Quan > 20 BEGIN PRINT 'Well stocked'; END
ELSE IF  @Quan >= 10 AND @Quan <= 20 BEGIN PRINT 'Moderate stock'; END
ELSE IF  @Quan < 10  BEGIN PRINT 'Low stock - reorder needed'; END

------------------------------------------------------------------------------------------------------------------------------------
-- *** 6.Create a WHILE loop that updates low-stock items (quantity < 5) in batches of 3 products at a time.
--Add 10 units to each product and display progress messages after each batch.#
DECLARE @quantity INT = 0
SELECT @quantity = quantity FROM production.stocks WHERE quantity < 5;
WHILE EXISTS (
    SELECT 1 FROM production.stocks WHERE quantity < 5
)
BEGIN
	 UPDATE production.stocks
    SET quantity = quantity + 10
	WHERE product_id IN (
        SELECT TOP 3 product_id
        FROM production.stocks
        WHERE quantity < 5
        ORDER BY product_id
    );
	PRINT 'Batch updated';
END

------------------------------------------------------------------------------------------------------------------------------------
--7. Product Price Categorization#
--Write a query that categorizes all products using CASE WHEN based on their list price:
--Under $300: Budget
--$300-$800: Mid-Range
--$801-$2000: Premium
--Over $2000: Luxury

SELECT product_name, list_price,
        (CASE
	        WHEN list_price <300 THEN 'Budget'
			WHEN list_price BETWEEN 300 AND 800 THEN 'Mid-Range'
			WHEN list_price BETWEEN 801 AND 2000 THEN 'Premium'
			WHEN list_price > 2000 THEN 'Luxury'
	   END) AS Price_Categorization
FROM production.products

------------------------------------------------------------------------------------------------------------------------------------
-- 8. Customer Order Validation#
--Create a query that checks if customer ID 5 exists in the database. If they exist, show their order count. If not, display an appropriate message.
-- Check if customer exists before creating an order

DECLARE @customer_id INT = 5;
IF EXISTS (SELECT 1 FROM sales.customers WHERE customer_id = @customer_id)
BEGIN
    PRINT 'Customer found. Proceeding with order creation...';
    SELECT count(order_id) AS Orders_Count 
    FROM sales.orders
	WHERE customer_id=@customer_id
END
ELSE
BEGIN
    PRINT 'Customer not found. Please create customer record first.';
END
GO
------------------------------------------------------------------------------------------------------------------------------------
-- 9. Shipping Cost Calculator Function#
--Create a scalar function named CalculateShipping that takes an order total as input and returns shipping cost:
--Orders over $100: Free shipping ($0)
--Orders $50-$99: Reduced shipping ($5.99)
--Orders under $50: Standard shipping ($12.99)
CREATE FUNCTION dbo.CalculateShipping (@order_total DECIMAL(10,2))
RETURNS DECIMAL(5,2)
AS
BEGIN
    DECLARE @shipping_cost DECIMAL(5,2);

    IF @order_total > 100
        SET @shipping_cost = 0.00;
    ELSE IF @order_total BETWEEN 50 AND 99.99
        SET @shipping_cost = 5.99;
    ELSE
        SET @shipping_cost = 12.99;

    RETURN @shipping_cost;
END;
GO
SELECT dbo.CalculateShipping(120.00) AS ShippingCost; 

------------------------------------------------------------------------------------------------------------------------------------
-- 10. Product Category Function#
--Create an inline table-valued function named GetProductsByPriceRange that accepts minimum and maximum price parameters 
--and returns all products within that price range with their brand and category information.

GO
CREATE FUNCTION dbo.GetProductsByPriceRange(@min INT,@max INT)
RETURNS TABLE
AS
RETURN
(
     SELECT 
        p.product_name,
        p.list_price,
        b.brand_name,
        c.category_name
    FROM production.products p
    JOIN production.brands b ON p.brand_id = b.brand_id
    JOIN production.categories c ON p.category_id = c.category_id
    WHERE p.list_price BETWEEN @min AND @max
);
GO
SELECT * FROM dbo.GetProductsByPriceRange(1000,20000); 

------------------------------------------------------------------------------------------------------------------------------------
-- **** 11. Customer Sales Summary Function#
--Create a multi-statement function named GetCustomerYearlySummary that takes a customer ID and returns a table with yearly sales data
--including total orders, total spent, and average order value for each year.
GO
CREATE FUNCTION dbo.GetCustomerYearlySummary(@customer_id INT)
RETURNS @summary TABLE
(
    Order_Year INT,
    Total_Number_of_Orders INT,
    Total_Amount_Spent DECIMAL(10,2),
    Avg_Order_Value DECIMAL(10,2)

)
AS
BEGIN
INSERT INTO @summary
        SELECT
            YEAR(o.order_date) as Order_Year,
            COUNT(*) as Total_Number_of_Orders,
            SUM(oi.quantity * oi.list_price * (1 - oi.discount)) as Total_Amount_Spent,
           AVG(oi.quantity * oi.list_price * (1 - oi.discount)) as Avg_Order_Value
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    WHERE o.customer_id = @customer_id
    GROUP BY YEAR(o.order_date);
    RETURN;
END;
GO
SELECT * FROM dbo.GetCustomerYearlySummary(1);

------------------------------------------------------------------------------------------------------------------------------------
-- 12. Discount Calculation Function#
--Write a scalar function named CalculateBulkDiscount that determines discount percentage based on quantity:
--1-2 items: 0% discount
--3-5 items: 5% discount
--6-9 items: 10% discount
--10+ items: 15% discount
GO
CREATE FUNCTION dbo.CalculateBulkDiscount (@quantity INT)
RETURNS VARCHAR(25)
AS
BEGIN
    DECLARE @discount VARCHAR(25);

    IF @quantity in (1,2) 
        SET @discount = '0%' ;
    ELSE IF @quantity BETWEEN 3 AND 5
        SET @discount = '5%';
    ELSE IF @quantity BETWEEN 6 AND 9
        SET @discount = '10%';
    ELSE
        SET @discount = '15%';

    RETURN @discount;
END;
GO
SELECT dbo.CalculateBulkDiscount(3) AS Discount; 

------------------------------------------------------------------------------------------------------------------------------------
--13. Customer Order History Procedure#
-- Create a stored procedure named sp_GetCustomerOrderHistory that accepts a customer ID and optional start/end dates. 
--Return the customer's order history with order totals calculated.


GO
CREATE PROCEDURE sp_GetCustomerOrderHistory
    @customer_id INT,
    @start_date DATE = NULL,
    @end_date DATE = NULL
AS
BEGIN
    SELECT
    o.order_id,
    o.order_date,
    o.order_status,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) as order_total
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    WHERE o.customer_id = @customer_id
        AND (@start_date IS NULL OR o.order_date >= @start_date)
        AND (@end_date IS NULL OR o.order_date <= @end_date)
    GROUP BY o.order_id, o.order_date, o.order_status
    ORDER BY o.order_date DESC;
END;
-- Usage
GO
EXEC sp_GetCustomerOrderHistory @customer_id = 1;
EXEC sp_GetCustomerOrderHistory @customer_id = 1, @start_date = '2017-01-01';
GO

------------------------------------------------------------------------------------------------------------------------------------
-- 14. Inventory Restock Procedure#
--Write a stored procedure named sp_RestockProduct with input parameters for store ID, product ID, and restock quantity. 
--Include output parameters for old quantity, new quantity, and success status.

CREATE PROCEDURE sp_RestockProduct
                 @store_id INT = 0,
				 @product_id INT = 0,
				 @restock_quantity DECIMAL(10,2) =0.00
AS 
BEGIN
     DECLARE @old_quantity DECIMAL(10,2) =0.00;
	 DECLARE @new_quantity DECIMAL(10,2) =0.00;
	 DECLARE @success_status INT = 0;
     SELECT 
	       @old_quantity = quantity
	 FROM production.stocks
	 WHERE store_id = @store_id AND product_id = @product_id;
	 -------------****--------------
   IF @old_quantity IS NOT NULL
    BEGIN
        SET @new_quantity = @old_quantity + @restock_quantity;

        UPDATE production.stocks
        SET quantity = @new_quantity
        WHERE store_id = @store_id AND product_id = @product_id;
        SET @success_status = 1;
    END
    ELSE
    BEGIN
        SET @old_quantity = 0;
        SET @new_quantity = 0;
        SET @success_status = 0;
    END
	----------------****-----------------
	   SELECT @old_quantity AS Old_Quantity,
       @new_quantity AS New_Quantity,
       @success_status AS Success_Status;
END;
GO
EXEC sp_RestockProduct 
    @store_id = 1, 
    @product_id = 101, 
    @restock_quantity = 25.00;
GO
------------------------------------------------------------------------------------------------------------------------------------
--***** 15. Order Processing Procedure#
--Create a stored procedure named sp_ProcessNewOrder that handles complete order creation with proper transaction control and error handling. 
--Include parameters for customer ID, product ID, quantity, and store ID.
 CREATE PROCEDURE sp_ProcessNewOrder
    @CustomerID INT,
    @ProductID INT,
    @Quantity INT,
    @StoreID INT,
    @OrderID INT OUTPUT,
    @Success BIT OUTPUT,
    @Message NVARCHAR(255) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Check customer exists
        IF NOT EXISTS (SELECT 1 FROM sales.customers WHERE customer_id = @CustomerID)
        BEGIN
            SET @Success = 0;
            SET @Message = 'Customer does not exist';
            ROLLBACK;
            RETURN;
        END
        
        -- Check product exists
        IF NOT EXISTS (SELECT 1 FROM production.products WHERE product_id = @ProductID)
        BEGIN
            SET @Success = 0;
            SET @Message = 'Product does not exist';
            ROLLBACK;
            RETURN;
        END
        
        -- Check store exists
        IF NOT EXISTS (SELECT 1 FROM sales.stores WHERE store_id = @StoreID)
        BEGIN
            SET @Success = 0;
            SET @Message = 'Store does not exist';
            ROLLBACK;
            RETURN;
        END
        
        -- Check inventory
        DECLARE @AvailableQty INT;
        SELECT @AvailableQty = quantity 
        FROM production.stocks 
        WHERE product_id = @ProductID AND store_id = @StoreID;
        
        IF @AvailableQty < @Quantity
        BEGIN
            SET @Success = 0;
            SET @Message = CONCAT('Insufficient inventory. Available: ', ISNULL(@AvailableQty, 0));
            ROLLBACK;
            RETURN;
        END
        
        -- Create order
        DECLARE @OrderDate DATE = GETDATE();
        DECLARE @Status TINYINT = 1; -- Order Received
        
        INSERT INTO sales.orders (
            customer_id, order_status, order_date, 
            required_date, store_id, staff_id
        )
        VALUES (
            @CustomerID, @Status, @OrderDate,
            DATEADD(day, 7, @OrderDate), @StoreID, 1 -- Assuming staff_id 1
        );
        
        SET @OrderID = SCOPE_IDENTITY();
        
        -- Add order item
        DECLARE @ListPrice DECIMAL(10,2);
        DECLARE @Discount DECIMAL(4,2) = dbo.CalculateBulkDiscount(@Quantity);
        
        SELECT @ListPrice = list_price
        FROM production.products
        WHERE product_id = @ProductID;
        
        INSERT INTO sales.order_items (
            order_id, item_id, product_id, 
            quantity, list_price, discount
        )
        VALUES (
            @OrderID, 1, @ProductID,
            @Quantity, @ListPrice, @Discount
        );
        
        -- Update inventory
        UPDATE production.stocks
        SET quantity = quantity - @Quantity
        WHERE product_id = @ProductID AND store_id = @StoreID;
        
        SET @Success = 1;
        SET @Message = 'Order processed successfully';
        COMMIT;
    END TRY
    BEGIN CATCH
        SET @Success = 0;
        SET @Message = ERROR_MESSAGE();
        ROLLBACK;
    END CATCH
END;
DECLARE @OrderID INT;
DECLARE @Success BIT;
DECLARE @Message NVARCHAR(255);
EXEC sp_ProcessNewOrder 
    @CustomerID = 1,
    @ProductID = 2,
    @Quantity = 3,
    @StoreID = 1,
    @OrderID = @OrderID OUTPUT,
    @Success = @Success OUTPUT,
    @Message = @Message OUTPUT;

SELECT 
    @OrderID AS OrderID,
    @Success AS Success,
    @Message AS Message;

------------------------------------------------------------------------------------------------------------------------------------
-- 16. **** Dynamic Product Search Procedure#
--Write a stored procedure named sp_SearchProducts that builds dynamic SQL based on optional parameters: product name search term,
--category ID, minimum price, maximum price, and sort column.
GO
CREATE PROCEDURE sp_SearchProducts
    @name VARCHAR(100) = NULL,
    @category_id INT = NULL,
    @min_price DECIMAL(10,2) = NULL,
    @max_price DECIMAL(10,2) = NULL,
    @sort_column VARCHAR(50) = NULL
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);
    DECLARE @search_name VARCHAR(100);

    IF @name IS NOT NULL
        SET @search_name = '%' + @name + '%';      -- to show every product contain the target name
    ELSE
        SET @search_name = NULL;
    SET @sql = N'
        SELECT product_id, product_name, category_id, list_price
        FROM production.products
        WHERE 1 = 1';
    IF @name IS NOT NULL
        SET @sql += N' AND product_name LIKE @search_name';
    IF @category_id IS NOT NULL
        SET @sql += N' AND category_id = @cat_id';
    IF @min_price IS NOT NULL
        SET @sql += N' AND list_price >= @min_price';
    IF @max_price IS NOT NULL
        SET @sql += N' AND list_price <= @max_price';
    
    IF @sort_column IN ('product_name', 'list_price', 'category_id')
        SET @sql += N' ORDER BY ' + QUOTENAME(@sort_column);
    ELSE
        SET @sql += N' ORDER BY product_name';

    EXEC sp_executesql 
        @sql,
        N'@search_name VARCHAR(100), @cat_id INT, @min_price DECIMAL(10,2), @max_price DECIMAL(10,2)',
        @search_name = @search_name,
        @cat_id = @category_id,
        @min_price = @min_price,
        @max_price = @max_price;
END;
GO

EXEC sp_SearchProducts @name = 'Skirt';
GO
------------------------------------------------------------------------------------------------------------------------------------
--17. ** Staff Bonus Calculation System#
--Create a complete solution that calculates quarterly bonuses for all staff members. Use variables to store date ranges and bonus rates.
--Apply different bonus percentages based on sales performance tiers.

DECLARE 
    @start_date DATE = '2023-01-01',
    @end_date DATE = '2023-03-31', -- Q1
    @low_bonus DECIMAL(5,2) = 0.02,
    @mid_bonus DECIMAL(5,2) = 0.05,
    @high_bonus DECIMAL(5,2) = 0.10;

-- Calculate sales and bonuses per staff
SELECT 
    s.staff_id,
    s.first_name + ' ' + s.last_name AS staff_name,
    SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_sales,

    -- Apply bonus tiers
    CASE 
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) < 10000 THEN 
            SUM(oi.quantity * oi.list_price * (1 - oi.discount)) * @low_bonus
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) BETWEEN 10000 AND 50000 THEN 
            SUM(oi.quantity * oi.list_price * (1 - oi.discount)) * @mid_bonus
        ELSE 
            SUM(oi.quantity * oi.list_price * (1 - oi.discount)) * @high_bonus
    END AS bonus_amount

FROM sales.staffs s
JOIN sales.orders o ON s.staff_id = o.staff_id
JOIN sales.order_items oi ON o.order_id = oi.order_id

WHERE o.order_date BETWEEN @start_date AND @end_date

GROUP BY s.staff_id, s.first_name, s.last_name;

------------------------------------------------------------------------------------------------------------------------------------
--18. Smart Inventory Management#
--Write a complex query with nested IF statements that manages inventory restocking.
--Check current stock levels and apply different reorder quantities based on product categories and current stock levels.
SELECT   
    p.product_id,
    p.product_name,
    p.category_id,
    s.quantity AS current_stock,
	(CASE 
        WHEN s.quantity < 10 THEN
            CASE 
                WHEN p.category_id = 1 THEN 50         
                WHEN p.category_id = 2 THEN 100        
                ELSE 30                                
            END
        WHEN s.quantity BETWEEN 10 AND 20 THEN 20      
        ELSE 0 
		END) AS suggested_reorder
FROM production.products p
JOIN production.stocks s 
ON p.product_id = s.product_id;



------------------------------------------------------------------------------------------------------------------------------------
-- 19. Customer Loyalty Tier Assignment#
--Create a comprehensive solution that assigns loyalty tiers to customers based on their total spending. 
--Handle customers with no orders appropriately and use proper NULL checking.

SELECT 
    c.customer_id,
    c.first_name + ' ' + c.last_name AS full_name,

    -- Total spent: handle NULL if no orders
    ISNULL(SUM(oi.quantity * oi.list_price * (1 - oi.discount)), 0) AS total_spent,

    -- Loyalty tier assignment
    CASE 
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) IS NULL THEN 'No Orders'
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) < 1000 THEN 'Bronze'
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) BETWEEN 1000 AND 4999.99 THEN 'Silver'
        WHEN SUM(oi.quantity * oi.list_price * (1 - oi.discount)) BETWEEN 5000 AND 9999.99 THEN 'Gold'
        ELSE 'Platinum'
    END AS loyalty_tier

FROM sales.customers c
LEFT JOIN sales.orders o ON c.customer_id = o.customer_id
LEFT JOIN sales.order_items oi ON o.order_id = oi.order_id

GROUP BY c.customer_id, c.first_name, c.last_name;
GO
------------------------------------------------------------------------------------------------------------------------------------
-- 20. Product Lifecycle Management#
--Write a stored procedure that handles product discontinuation including checking for pending orders, 
--optional product replacement in existing orders, clearing inventory, and providing detailed status messages.
CREATE PROCEDURE sp_DiscontinueProduct
    @product_id INT,
    @replacement_id INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- Check if the product exists
    IF NOT EXISTS (SELECT 1 FROM production.products WHERE product_id = @product_id)
    BEGIN
        PRINT 'Product not found.';
        RETURN;
    END
    -- Check if there are any pending orders (status = 1)
    IF EXISTS (
        SELECT 1
        FROM sales.orders o
        JOIN sales.order_items oi ON o.order_id = oi.order_id
        WHERE oi.product_id = @product_id AND o.order_status = 1
    )
    BEGIN
        -- If a replacement product is provided
        IF @replacement_id IS NOT NULL
        BEGIN
            -- Check if the replacement product exists
            IF NOT EXISTS (SELECT 1 FROM production.products WHERE product_id = @replacement_id)
            BEGIN
                PRINT 'Replacement product not found.';
                RETURN;
            END
            -- Replace the product in pending orders
            UPDATE oi
            SET product_id = @replacement_id
            FROM sales.order_items oi
            JOIN sales.orders o ON oi.order_id = o.order_id
            WHERE oi.product_id = @product_id AND o.order_status = 1;
            PRINT 'Product replaced in pending orders.';
        END
        ELSE
        BEGIN
            PRINT 'Cannot discontinue: product is in pending orders and no replacement was provided.';
            RETURN;
        END
    END
    --Clear inventory for the discontinued product
    UPDATE production.stocks
    SET quantity = 0
    WHERE product_id = @product_id;
    PRINT 'Inventory cleared for discontinued product.';
END;
GO
EXEC sp_DiscontinueProduct @product_id = 101;
EXEC sp_DiscontinueProduct @product_id = 101, @replacement_id = 205;



                 


