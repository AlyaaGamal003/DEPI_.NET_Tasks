-- Customer activity log
CREATE TABLE sales.customer_log (
    log_id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT,
    action VARCHAR(50),
    log_date DATETIME DEFAULT GETDATE()
);
go
-- Price history tracking
CREATE TABLE production.price_history (
    history_id INT IDENTITY(1,1) PRIMARY KEY,
    product_id INT,
    old_price DECIMAL(10,2),
    new_price DECIMAL(10,2),
    change_date DATETIME DEFAULT GETDATE(),
    changed_by VARCHAR(100)
);
go
-- Order audit trail
CREATE TABLE sales.order_audit (
    audit_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    customer_id INT,
    store_id INT,
    staff_id INT,
    order_date DATE,
    audit_timestamp DATETIME DEFAULT GETDATE()
);
go
INSERT INTO sales.customer_log (customer_id, action, log_date)
VALUES 
(101, 'Login', '2025-07-20 09:15:00'),
(102, 'View Product', '2025-07-20 09:20:00'),
(101, 'Add to Cart', '2025-07-20 09:25:00'),
(103, 'Register', '2025-07-19 14:10:00'),
(101, 'Checkout', '2025-07-20 09:30:00'),
(104, 'Login', '2025-07-18 18:00:00'),
(102, 'Logout', '2025-07-20 09:45:00'),
(105, 'View Product', '2025-07-21 10:15:00');
go
INSERT INTO production.price_history (product_id, old_price, new_price, change_date, changed_by)
VALUES 
(201, 49.99, 44.99, '2025-07-15 08:00:00', 'admin@store.com'),
(202, 15.00, 17.50, '2025-07-16 10:30:00', 'manager@store.com'),
(203, 100.00, 95.00, '2025-07-17 14:45:00', 'pricing_team@store.com'),
(204, 25.99, 25.99, '2025-07-18 11:20:00', 'admin@store.com'), -- no change, just confirmation
(205, 9.99, 8.49, '2025-07-19 09:00:00', 'admin@store.com');
go
INSERT INTO sales.order_audit (order_id, customer_id, store_id, staff_id, order_date, audit_timestamp)
VALUES 
(1001, 101, 1, 501, '2025-07-20', '2025-07-20 09:35:00'),
(1002, 102, 2, 502, '2025-07-20', '2025-07-20 09:50:00'),
(1003, 103, 1, 501, '2025-07-19', '2025-07-19 14:30:00'),
(1004, 104, 3, 503, '2025-07-18', '2025-07-18 18:10:00'),
(1005, 105, 2, 502, '2025-07-21', '2025-07-21 10:30:00');
