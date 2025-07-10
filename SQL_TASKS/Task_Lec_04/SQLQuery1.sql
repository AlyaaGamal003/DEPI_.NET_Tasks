--1. Count the total number of products in the database.
SELECT COUNT(product_name) AS "Total Number Of Products"
FROM production.products;

--2. Find the average, minimum, and maximum price of all products.
SELECT AVG(list_price) AS "Average Price" , MIN(list_price) AS "Min Price" ,MAX(list_price) AS "Max Price" 
FROM production.products;

--3. Count how many products are in each category.
SELECT c.category_name, COUNT(p.product_id) AS total_products
FROM production.categories c
LEFT JOIN production.products p 
ON c.category_id = p.category_id
GROUP BY c.category_name;

--4. Find the total number of orders for each store.
SELECT store_id, COUNT(order_id) AS total_orders
FROM sales.orders
GROUP BY store_id;

--5. Show customer first names in UPPERCASE and last names in lowercase for the first 10 customers.
SELECT TOP(10) UPPER(first_name) AS Upper_First_name ,LOWER(last_name) AS Lower_last_name
FROM sales.customers;

--6. Get the length of each product name. Show product name and its length for the first 10 products.
SELECT TOP(10) product_name, LEN(product_name) AS " Length Of Product Name"
FROM production.products;

--7. Format customer phone numbers to show only the area code (first 3 digits) for customers 1-15.
SELECT TOP(15) Left(phone,3) AS "Area Code"
FROM sales.customers;

--8. Show the current date and extract the year and month from order dates for orders 1-10.
SELECT order_date, YEAR(order_date) AS Order_Year ,MONTH(order_date) AS Order_Month
FROM sales.orders;

--9. Join products with their categories. Show product name and category name for first 10 products.
SELECT TOP(10) p.product_name,c.category_name
FROM production.products p 
JOIN production.categories c 
ON p.category_id=c.category_id;

--10. Join customers with their orders. Show customer name and order date for first 10 orders.
SELECT TOP(10) c.first_name+' '+c.last_name AS "Customer Name" , o.order_date
FROM sales.customers c 
JOIN sales.orders o
ON c.customer_id=o.customer_id;

--11. Show all products with their brand names, even if some products don't have brands. Include product name, brand name (show 'No Brand' if null).
SELECT p.product_name,
       COALESCE(b.brand_name, 'No brand details') AS Brand_Name
FROM production.products p 
LEFT JOIN production.brands b
ON p.brand_id=b.brand_id;

--12. Find products that cost more than the average product price. Show product name and price.
SELECT product_name, list_price
FROM production.products
WHERE list_price >(
      SELECT AVG(list_price)
	  FROM production.products
	  )
ORDER BY list_price DESC;

--13. Find customers who have placed at least one order. Use a subquery with IN. Show customer_id and customer_name.
SELECT customer_id,first_name+' '+last_name AS "Customer Name"
FROM sales.customers
WHERE customer_id IN (
        SELECT customer_id
        FROM sales.orders
        WHERE customer_id IS NOT NULL
    );

--14. For each customer, show their name and total number of orders using a subquery in the SELECT clause.
SELECT 
    c.first_name + ' ' + c.last_name AS customer_name,
    (
        SELECT COUNT(*) 
        FROM sales.orders o 
        WHERE o.customer_id = c.customer_id
    ) AS total_orders
FROM sales.customers c;

Go
--15. Create a simple view called easy_product_list that shows product name, category name, and price. Then write a query to select all products from this view where price > 100.
CREATE VIEW easy_product_list AS
SELECT
    p.product_name,
    c.category_name,
    p.list_price
FROM production.products p
INNER JOIN production.categories c ON p.category_id = c.category_id;
Go
-- Use the view
SELECT * FROM easy_product_list
WHERE list_price > 100
ORDER BY list_price DESC;

GO
--16. Create a view called customer_info that shows customer ID, full name (first + last), email, and city and state combined. Then use this view to find all customers from California (CA).

CREATE VIEW customer_info AS
SELECT
    c.customer_id,
    c.first_name + ' ' + c.last_name as customer_name,
    c.email,
    c.city,
    c.state
FROM sales.customers c;

GO
-- Use the view
SELECT * FROM customer_info
WHERE state = 'CA'
ORDER BY customer_name;

--17. Find all products that cost between $50 and $200. Show product name and price, ordered by price from lowest to highest.
SELECT product_name, list_price
FROM production.products
WHERE list_price between 50 AND 200 
ORDER BY list_price ASC;

--18. Count how many customers live in each state. Show state and customer count, ordered by count from highest to lowest.
SELECT COUNT(customer_id) AS customer_count, state
FROM sales.customers
GROUP BY state
ORDER BY COUNT(customer_id) DESC;

--19. Find the most expensive product in each category. Show category name, product name, and price.
SELECT c.category_name, p.product_name, p.list_price
FROM  production.products p
JOIN  production.categories c 
ON p.category_id = c.category_id
WHERE p.list_price = (
        SELECT MAX(p2.list_price)
        FROM production.products p2
        WHERE p2.category_id = p.category_id);

--20. Show all stores and their cities, including the total number of orders from each store. Show store name, city, and order count.
SELECT  s.store_name, s.city, COUNT(o.order_id) AS order_count
FROM  sales.stores s 
LEFT JOIN sales.orders o 
ON s.store_id = o.store_id
GROUP BY s.store_name, s.city;
