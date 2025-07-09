--1) List all products with list price greater than 1000
SELECT product_name , list_price 
FROM production.products 
WHERE list_price> 1000;
----------------------------------------------------------------------------------
--2) Get customers from "CA" or "NY" states
SELECT first_name + ' ' +last_name as "Customer Name" ,state
FROM sales.customers
Where State in('NY','CA');
----------------------------------------------------------------------------------
--3) Retrieve all orders placed in 2023
SELECT *
FROM sales.orders
WHERE YEAR(order_date) = 2023;
----------------------------------------------------------------------------------
--4) Show customers whose emails end with @gmail.com
SELECT first_name+ ' '+ last_name AS "Customer Name" 
FROM sales.customers
WHERE email LIKE '%@gmail.com';
----------------------------------------------------------------------------------
--5) Show all inactive staff
SELECT first_name+ ' '+ last_name AS "Staff Name" 
FROM sales.staffs
WHERE active=0;
----------------------------------------------------------------------------------
--6) List top 5 most expensive products
SELECT TOP (5) product_name
FROM production.products
Order by list_price DESC
----------------------------------------------------------------------------------
--7) Show latest 10 orders sorted by date
SELECT TOP (10) * FROM sales.orders
ORDER BY order_date DESC
----------------------------------------------------------------------------------
--8) Retrieve the first 3 customers alphabetically by last name
SELECT top(3)first_name+ ' '+ last_name AS "Customer Name" 
FROM sales.customers
ORDER BY last_name ASC
----------------------------------------------------------------------------------
--9) Find customers who did not provide a phone number
SELECT first_name+ ' '+ last_name AS "Customer Name" 
FROM sales.customers
WHERE phone is NULL;
----------------------------------------------------------------------------------
--10) Show all staff who have a manager assigned
SELECT first_name+ ' '+ last_name AS "Staff Name",manager_id
FROM sales.staffs
WHERE manager_id is NOT NULL;
----------------------------------------------------------------------------------
--11) Count number of products in each category
SELECT 
    c.category_id,
    c.category_name,
    COUNT(p.product_id) AS product_count
FROM production.categories c
LEFT JOIN  production.products p ON c.category_id = p.category_id
GROUP BY  c.category_id, c.category_name
ORDER BY  product_count DESC, c.category_name;
----------------------------------------------------------------------------------
--12) Count number of customers in each state
SELECT state,COUNT(customer_id) AS customer_count
FROM sales.customers
WHERE state IS NOT NULL
GROUP BY state
ORDER BY customer_count DESC;
----------------------------------------------------------------------------------
--13) Get average list price of products per brand
SELECT AVG(p.list_price) AS "AVG_List_Price",b.brand_name
FROM production.products p JOIN production.brands b ON
p.brand_id=b.brand_id
GROUP BY brand_name
----------------------------------------------------------------------------------
--14) Show number of orders per staff
SELECT  s.first_name+ ' '+ s.last_name AS "Staff Name", count(o.order_id) AS "Number Of Order Per Staff" 
FROM sales.orders o
JOIN sales.staffs s ON o.staff_id=s.staff_id
GROUP BY s.first_name ,s.last_name
----------------------------------------------------------------------------------
--15) Find customers who made more than 2 orders
SELECT  c.first_name+ ' '+ c.last_name AS "Customer Name" ,count (o.order_id) AS "Number of Orders"
FROM sales.customers c JOIN sales.orders o on c.customer_id=o.customer_id
GROUP BY c.first_name,c.last_name
HAVING COUNT(o.order_id) > 2;
----------------------------------------------------------------------------------
--16) Products priced between 500 and 1500
SELECT product_name,list_price 
FROM production.products
WHERE list_price between 500 AND 1500
----------------------------------------------------------------------------------
--17) Customers in cities starting with "S"
SELECT  first_name+ ' '+ last_name AS "Customer Name" ,city
FROM sales.customers 
WHERE city like 'S%'
----------------------------------------------------------------------------------
--18) Orders with order_status either 2 or 4
SELECT order_id,order_status
FROM sales.orders
WHERE order_status in (2,4)
----------------------------------------------------------------------------------
--19) Products from category_id IN (1, 2, 3)
SELECT product_name,list_price,category_id
FROM production.products
WHERE category_id IN (1, 2, 3);
----------------------------------------------------------------------------------
--20) Staff working in store_id = 1 OR without phone number
SELECT  first_name+ ' '+ last_name AS "Staff Name" 
FROM sales.staffs
WHERE store_id =1 OR phone IS NULL
