-- 1.Create a non-clustered index on the email column in the sales.customers table to improve search performance when looking up customers by email.

CREATE NONCLUSTERED INDEX IX_Customers_Email 
ON sales.customers (email)
INCLUDE (first_name, last_name, phone);
--------------------------------------------------------------------------------------------------------------------
-- 2.Create a composite index on the production.products table 
-- that includes category_id and brand_id columns to optimize searches that filter by both category and brand.

CREATE NONCLUSTERED INDEX IX_Products_Brand_Category
ON production.products (brand_id, category_id)
INCLUDE (product_name, list_price);
--------------------------------------------------------------------------------------------------------------------
-- 3.Create an index on sales.orders table for the order_date column and include customer_id, store_id,
-- and order_status as included columns to improve reporting queries.Index for order date searches

CREATE NONCLUSTERED INDEX IX_Orders_OrderDate
ON sales.orders (order_date)
INCLUDE (customer_id, store_id, order_status);
--------------------------------------------------------------------------------------------------------------------
-- 4.Create a trigger that automatically inserts a welcome record into a customer_log table
-- whenever a new customer is added to sales.customers. (First create the log table, then the trigger)
go
CREATE TRIGGER tr_Customer_WelcomeLog
ON sales.customers
AFTER INSERT
AS
BEGIN
    INSERT INTO sales.customer_log (customer_id, action)
    SELECT customer_id, 'Welcome'
    FROM inserted;
END;
--------------------------------------------------------------------------------------------------------------------
-- 5.Create a trigger on production.products that logs any changes to the list_price column into a price_history table
-- , storing the old price, new price, and change date.
go
CREATE TRIGGER tr_LogPriceChange
ON production.products
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO production.price_history (product_id, old_price, new_price, change_date, changed_by)
    SELECT 
        i.product_id,
        d.list_price AS old_price,
        i.list_price AS new_price,
        GETDATE(),
        SYSTEM_USER
    FROM inserted i
    JOIN deleted d ON i.product_id = d.product_id
    WHERE i.list_price <> d.list_price;
END;

--------------------------------------------------------------------------------------------------------------------
-- 6.Create an INSTEAD OF DELETE trigger on production.categories 
-- that prevents deletion of categories that have associated products. Display an appropriate error message.
go
CREATE TRIGGER tr_PreventCategoryDelete
ON production.categories
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM deleted d
        JOIN production.products p ON p.category_id = d.category_id
    )
    BEGIN
        RAISERROR ('Cannot delete category: products are still assigned to it.', 16, 1);
        RETURN;
    END
    DELETE FROM production.categories
    WHERE category_id IN (SELECT category_id FROM deleted);
END;
--------------------------------------------------------------------------------------------------------------------
-- 7.Create a trigger on sales.order_items that automatically reduces the quantity in production.
-- stocks when a new order item is inserted.
go
CREATE TRIGGER tr_ReduceStockOnOrder
ON sales.order_items
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE s
    SET s.quantity = s.quantity - i.quantity
    FROM production.stocks s
    INNER JOIN inserted i 
        ON s.product_id = i.product_id AND s.store_id = i.order_id;

    IF EXISTS (
        SELECT 1 FROM production.stocks WHERE quantity < 0
    )
    BEGIN
        RAISERROR ('Order quantity exceeds available stock.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;

--------------------------------------------------------------------------------------------------------------------
--8.Create a trigger that logs all new orders into an order_audit table, 
-- capturing order details and the date/time when the record was created.
go
CREATE TRIGGER tr_LogNewOrder
ON sales.orders
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO sales.order_audit (order_id, customer_id, store_id, staff_id, order_date)
    SELECT order_id, customer_id, store_id, staff_id, order_date
    FROM inserted;
END;
