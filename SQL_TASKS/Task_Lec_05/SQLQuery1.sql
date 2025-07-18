-- 1.Write a query that classifies all products into price categories:
--Products under $300: "Economy"
--Products $300-$999: "Standard"
--Products $1000-$2499: "Premium"
--Products $2500 and above: "Luxury"

SELECT product_name,
(CASE 
    WHEN list_price <= 300 THEN 'Economy'
	WHEN list_price > 300 AND list_price <= 999 THEN 'Standard'
	WHEN list_price >= 1000 AND list_price < 2499 THEN 'Premium'
	WHEN list_price >= 2500 THEN 'Luxury'
	END) AS classification
from production.products;

-----------------------------------------------------------------------------------------------------
--2.Create a query that shows order processing information with user-friendly status descriptions:
--Status 1: "Order Received"
--Status 2: "In Preparation"
--Status 3: "Order Cancelled"
--Status 4: "Order Delivered"
--Also add a priority level:
--Orders with status 1 older than 5 days: "URGENT"
--Orders with status 2 older than 3 days: "HIGH"
--All other orders: "NORMAL"

SELECT order_status,
   (CASE 
        WHEN order_status = 1 THEN 'Order Received'
	    WHEN order_status = 2 THEN 'In Preparation'
        WHEN order_status = 3 THEN 'Order Cancelled'
	    WHEN order_status = 4 THEN 'Order Delivered'
   End) AS order_status_text,
   DATEDIFF(DAY, order_date, GETDATE()) AS days_old,
    (CASE 
        WHEN order_status = 1 AND DATEDIFF(DAY, order_date, GETDATE()) > 5 THEN 'URGENT'
        WHEN order_status = 2 AND DATEDIFF(DAY, order_date, GETDATE()) > 3 THEN 'HIGH'
        ELSE 'NORMAL'
    END) AS priority_level
from sales.orders;

----------------------------------------------------------------------------------------------------
--3.Write a query that categorizes staff based on the number of orders they've handled:
--0 orders: "New Staff"
--1-10 orders: "Junior Staff"
--11-25 orders: "Senior Staff"
--26+ orders: "Expert Staff"

Select s.first_name + ' '+ s.last_name AS Staff_Name ,count(o.order_id) AS Orders_Numbers,
       (CASE
	    WHEN count(o.order_id) = 0 THEN 'New Staff'
	    WHEN count(o.order_id) >= 1  AND count(o.order_id) <= 10 THEN 'Junior Staff'
		WHEN count(o.order_id) >= 11 AND count(o.order_id) <= 25 THEN 'Senior Staff'
	    WHEN count(o.order_id) >= 26 THEN 'Expert Staff'
	   END) AS Staff_Expert
FROM sales.staffs s 
Left join sales.orders o 
ON s.staff_id= o.staff_id
Group By s.first_name,s.last_name;
----------------------------------------------------------------------------------------------------
--4.Create a query that handles missing customer contact information:
--Use ISNULL to replace missing phone numbers with "Phone Not Available"
--Use COALESCE to create a preferred_contact field (phone first, then email, then "No Contact Method")
--Show complete customer information

SELECT first_name + ' ' +last_name AS Customer_Name,
ISNULL(phone,'Phone Not Available') AS Phone,
email,street,city,state,zip_code,
COALESCE(phone,email,'No Contact Method') AS preferred_contact
FROM sales.customers;
----------------------------------------------------------------------------------------------------
--5.Write a query that safely calculates price per unit in stock:
--Use NULLIF to prevent division by zero when quantity is 0
--Use ISNULL to show 0 when no stock exists
--Include stock status using CASE WHEN
--Only show products from store_id = 1
SELECT quantity,
ISNULL(oi.list_price / NULLIF(oi.quantity, 0), 0) AS Price,
       (CASE
	       WHEN oi.quantity = 0 THEN 'Out of Stock'
           WHEN oi.quantity <= 10 THEN 'Low Stock'
           ELSE 'In Stock'
	   END) AS stock_status
FROM sales.order_items oi join sales.orders o
ON oi.order_id=o.order_id
Where o.store_id=1;
----------------------------------------------------------------------------------------------------
--6.Create a query that formats complete addresses safely:
--Use COALESCE for each address component
--Create a formatted_address field that combines all components
--Handle missing ZIP codes gracefully
SELECT first_name + ' ' +last_name AS Customer_Name, CONCAT(
    COALESCE(street, ''), ' ',
    COALESCE(city, ''), ' ',
    COALESCE(state, '')
  ) AS formatted_address, 
ISNULL(zip_code,'No Zip Code') AS Zip_Code
FROM sales.customers;

----------------------------------------------------------------------------------------------------
--7.Use a CTE to find customers who have spent more than $1,500 total:
--Create a CTE that calculates total spending per customer
--Join with customer information
--Show customer details and spending
--Order by total_spent descending
WITH customer_spending AS (
    SELECT 
        o.customer_id,
        SUM(oi.quantity * oi.list_price) AS total_spent
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY o.customer_id
)
SELECT 
    c.first_name + ' ' + c.last_name AS Customer_Name,
    c.email,
    c.phone,
    cs.total_spent
FROM customer_spending cs
JOIN sales.customers c ON cs.customer_id = c.customer_id
WHERE cs.total_spent > 1500
ORDER BY cs.total_spent DESC;

----------------------------------------------------------------------------------------------------
--8.Create a multi-CTE query for category analysis:
--CTE 1: Calculate total revenue per category
--CTE 2: Calculate average order value per category
--Main query: Combine both CTEs
--Use CASE to rate performance: >$50000 = "Excellent", >$20000 = "Good", else = "Needs Improvement"
WITH total_revenue_CTE AS(
     SELECT c.category_name,c.category_id,
	 SUM(oi.quantity * oi.list_price) AS total_revenue
	 FROM sales.order_items oi 
	 JOIN production.products p 
	 ON oi.product_id =p.product_id
	 JOIN production.categories c
	 ON p.category_id=c.category_id
	 GROUP BY c.category_name,c.category_id
),
avg_order_value_cte AS(
     SELECT  c.category_name,c.category_id,
	 AVG(oi.order_id) AS avg_order_value
	 FROM sales.order_items oi
     JOIN production.products p ON oi.product_id = p.product_id
     JOIN production.categories c ON p.category_id = c.category_id
	 GROUP By c.category_name, c.category_id
)
SELECT 
    tr.category_name,
    tr.total_revenue,
    ao.avg_order_value,
    CASE 
        WHEN tr.total_revenue > 50000 THEN 'Excellent'
        WHEN tr.total_revenue > 20000 THEN 'Good'
        ELSE 'Needs Improvement'
    END AS performance_rating
FROM total_revenue_cte tr
JOIN avg_order_value_cte ao ON tr.category_id = ao.category_id;

----------------------------------------------------------------------------------------------------
--9.Use CTEs to analyze monthly sales trends:
--CTE 1: Calculate monthly sales totals
--CTE 2: Add previous month comparison
--Show growth percentage

WITH monthly_sales AS (
  SELECT 
    FORMAT(o.order_date, 'yyyy-MM') AS sales_month,
    SUM(oi.quantity * oi.list_price) AS total_sales
  FROM sales.orders o
  JOIN sales.order_items oi ON o.order_id = oi.order_id
  GROUP BY FORMAT(o.order_date, 'yyyy-MM')
),
monthly_comparison AS (
  SELECT 
    sales_month,
    total_sales,
    LAG(total_sales) OVER (ORDER BY sales_month) AS prev_month_sales
    FROM monthly_sales
)
SELECT 
  sales_month,
  total_sales,
  prev_month_sales,
  ROUND(
    CASE 
      WHEN prev_month_sales IS NULL OR prev_month_sales = 0 THEN NULL
      ELSE ((total_sales - prev_month_sales) * 100.0) / prev_month_sales
    END, 2
  ) AS growth_percent
FROM monthly_comparison;

----------------------------------------------------------------------------------------------------
--10.Create a query that ranks products within each category:
--Use ROW_NUMBER() to rank by price (highest first)
--Use RANK() to handle ties
--Use DENSE_RANK() for continuous ranking
--Only show top 3 products per category
 WITH ranked_products AS (
  SELECT 
    p.product_id,
    p.product_name,
    c.category_id,
	c.category_name,
    p.list_price,
    ROW_NUMBER() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS Price_Row_Rank,
    RANK() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS Price_Rank,
    DENSE_RANK() OVER (PARTITION BY c.category_id ORDER BY p.list_price DESC) AS Price_Dense_Rank
  FROM production.products p
  JOIN production.categories c ON p.category_id = c.category_id
)

SELECT category_name,list_price, Price_Row_Rank,Price_Rank,Price_Dense_Rank
FROM ranked_products
WHERE Price_Row_Rank <= 3;

----------------------------------------------------------------------------------------------------
--11.Rank customers by their total spending:
--Calculate total spending per customer
--Use RANK() for customer ranking
--Use NTILE(5) to divide into 5 spending groups
--Use CASE for tiers: 1="VIP", 2="Gold", 3="Silver", 4="Bronze", 5="Standard"

SELECT 
      o.customer_id,
	  SUM(oi.list_price*oi.quantity * (1 - oi.discount)) AS total_spending,
	  RANK() OVER (ORDER BY SUM(oi.list_price * oi.quantity * (1 - oi.discount)) DESC) AS Customer_RANK,
	  NTILE(5) OVER (ORDER BY SUM(oi.quantity * oi.list_price ) DESC) AS quartile,
	  CASE NTILE(5) OVER (ORDER BY 
	  SUM(oi.list_price*oi.quantity * (1 - oi.discount)) DESC)
           WHEN 1 THEN 'VIP'
           WHEN 2 THEN 'Gold'
           WHEN 3 THEN 'Silver'
           WHEN 4 THEN 'Bronze'
           ELSE 'Standard'
     END AS Tier
From sales.orders o 
JOIN sales.order_items oi
ON o.order_id=oi.order_id
Group BY o.customer_id;
----------------------- Clean Version ----------------------------
WITH spending_summary AS (
    SELECT 
        o.customer_id,
        SUM(oi.list_price * oi.quantity * (1 - oi.discount)) AS total_spending
    FROM sales.orders o
    JOIN sales.order_items oi ON o.order_id = oi.order_id
    GROUP BY o.customer_id
)

SELECT 
    customer_id,
    total_spending,
    RANK() OVER (ORDER BY total_spending DESC) AS Customer_Rank,
    NTILE(5) OVER (ORDER BY total_spending DESC) AS Spending_Group,
    CASE 
        WHEN NTILE(5) OVER (ORDER BY total_spending DESC) = 1 THEN 'VIP'
        WHEN NTILE(5) OVER (ORDER BY total_spending DESC) = 2 THEN 'Gold'
        WHEN NTILE(5) OVER (ORDER BY total_spending DESC) = 3 THEN 'Silver'
        WHEN NTILE(5) OVER (ORDER BY total_spending DESC) = 4 THEN 'Bronze'
        ELSE 'Standard'
    END AS Tier
FROM spending_summary;

----------------------------------------------------------------------------------------------------
--12.Create a comprehensive store performance ranking:
--Rank stores by total revenue
--Rank stores by number of orders
--Use PERCENT_RANK() to show percentile performance

WITH total_revenue AS(
     SELECT 
	        o.store_id,
	        SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS total_revenue,
			COUNT(o.order_id) AS total_orders 
	 FROM sales.order_items oi 
	 JOIN sales.orders o
	 ON oi.order_id=o.order_id
	 GROUP BY o.store_id
)

SELECT 
       store_id,
	   total_revenue,
       RANK() OVER (ORDER BY total_revenue DESC) AS total_revenue_Rank,
	   RANK() OVER (ORDER BY total_orders DESC) AS total_orders_Rank,
	   PERCENT_RANK() OVER (ORDER BY total_revenue DESC) AS revenue_percentile,
       PERCENT_RANK() OVER (ORDER BY total_orders DESC) AS order_percentile
From total_revenue

----------------------------------------------------------------------------------------------------
--13.Create a PIVOT table showing product counts by category and brand:
--Rows: Categories
--Columns: Top 4 brands (Electra, Haro, Trek, Surly)
--Values: Count of products

SELECT *
FROM (
   
      SELECT 
           c.category_name,
	       b.brand_name,
	       COUNT(p.product_id) AS product_count
      FROM production.categories c
     JOIN production.products p
     ON c.category_id=p.category_id
     JOIN production.brands b
     ON p.brand_id=b.brand_id
     GROUP BY c.category_name, b.brand_name
) AS source
PIVOT (
   SUM(product_count) 
   FOR brand_name IN ([Puma], [Nike], [Zara], [Surly])
) AS pivot_table

----------------------------------------------------------------------------------------------------
--14.Create a PIVOT showing monthly sales revenue by store:
--Rows: Store names
--Columns: Months (Jan through Dec)
--Values: Total revenue
--Add a total column
SELECT  store_name,
    ISNULL([January], 0) + ISNULL([February], 0) + ISNULL([March], 0) +
    ISNULL([April], 0) + ISNULL([May], 0) + ISNULL([June], 0) +
    ISNULL([July], 0) + ISNULL([August], 0) + ISNULL([September], 0) +
    ISNULL([October], 0) + ISNULL([November], 0) + ISNULL([December], 0) 
    AS Total_Revenue,
    [January], [February], [March], [April], [May], [June],
    [July], [August], [September], [October], [November], [December]
FROM (
     SELECT 
           SUM (oi.quantity * oi.list_price * (1 - oi.discount)) AS monthly_sales_revenue,
	       DATENAME(MONTH,o.order_date) AS Order_Month,
	       s.store_name
     FROM sales.order_items oi 
	 JOIN sales.orders o 
	 ON oi.order_id=o.order_id
	 JOIN sales.stores s
	 ON o.store_id=s.store_id
	 GROUP BY s.store_name, DATENAME(MONTH,o.order_date) 
) AS source
PIVOT (
   SUM(monthly_sales_revenue) 
  FOR Order_Month IN (
        [January], [February], [March], [April], [May], [June],
        [July], [August], [September], [October], [November], [December]
    )
) AS pivot_table
ORDER BY store_name;
----------------------------------------------------------------------------------------------------
--15.PIVOT order statuses across stores:
--Rows: Store names
--Columns: Order statuses (Pending, Processing, Completed, Rejected)
--Values: Count of orders

SELECT *
FROM (
        SELECT 
		      s.store_name,
			  COUNT(o.order_id) AS order_count,
			  (CASE
			  WHEN o.order_status =1 Then 'Pending' 
			  WHEN o.order_status =2 Then 'Processing'
			  WHEN o.order_status =3 Then 'Completed'
			  WHEN o.order_status =4 Then 'Rejected'
			  END )AS order_status
		FROM sales.stores s
		JOIN sales.orders o
		ON s.store_id=o.store_id
		Group By s.store_name, o.order_status
) AS source
PIVOT (
   SUM(order_count) 
   FOR order_status IN ([Pending],[Processing],[Completed],[Rejected])
)AS pivot_table

----------------------------------------------------------------------------------------------------
--16.Create a PIVOT comparing sales across years:
--Rows: Brand names
--Columns: Years (2022, 2023, 2024)
--Values: Total revenue
--Include percentage growth calculations
SELECT * ,  
    CAST(([2023] - [2022]) * 100.0 / NULLIF([2022], 0) AS DECIMAL(5, 2)) AS Growth_2023_Percent,
    CAST(([2024] - [2023]) * 100.0 / NULLIF([2023], 0) AS DECIMAL(5, 2)) AS Growth_2024_Percent
FROM(         
		 SELECT 
		       b.brand_name,
			   YEAR(o.order_date) AS Order_Year,
			   SUM(oi.quantity * oi.list_price * (1 - oi.discount)) AS Total_Revenue
		 FROM production.brands b
		 JOIN production.products p
		 ON b.brand_id=p.brand_id
		 JOIN sales.order_items oi
		 ON p.product_id=oi.product_id
		 JOIN sales.orders o
		 ON oi.order_id=o.order_id
		 GROUP BY b.brand_name , YEAR(o.order_date)
)AS source

PIVOT (
   SUM(Total_Revenue) 
   FOR Order_Year IN ([2022],[2023],[2024])
)AS pivot_table
ORDER BY brand_name;

----------------------------------------------------------------------------------------------------
--17.Use UNION to combine different product availability statuses:
--Query 1: In-stock products (quantity > 0)
--Query 2: Out-of-stock products (quantity = 0 or NULL)
--Query 3: Discontinued products (not in stocks table)

SELECT product_id, 'In Stock' AS availability_status
FROM production.stocks
WHERE quantity > 0
UNION
SELECT product_id, 'Out Stock' AS availability_status
FROM production.stocks
WHERE quantity = 0 OR quantity IS NULL
UNION
SELECT product_id, 'Discontinued' AS availability_status
FROM production.products
WHERE product_id NOT IN (SELECT product_id FROM production.stocks)

----------------------------------------------------------------------------------------------------
--18.Use INTERSECT to find loyal customers:
--Find customers who bought in both 2022 AND 2023
--Show their purchase patterns

SELECT customer_id
FROM sales.orders
WHERE YEAR(order_date) = 2022
INTERSECT
SELECT customer_id
FROM sales.orders
WHERE YEAR(order_date) = 2023

----------------------------------------------------------------------------------------------------
--19.Use multiple set operators to analyze product distribution:
--INTERSECT: Products available in all 3 stores
--EXCEPT: Products available in store 1 but not in store 2
--UNION: Combine above results with different labels


SELECT product_id, 'In All Stores' AS availability_type
FROM (
    SELECT product_id FROM production.stocks WHERE store_id=1
    INTERSECT
    SELECT product_id FROM production.stocks WHERE store_id=2
    INTERSECT
    SELECT product_id FROM production.stocks WHERE store_id=3
) AS all_stores
UNION
SELECT product_id, 'Only in Store 1' AS availability_type
FROM (
    SELECT product_id FROM production.stocks WHERE store_id=1
    EXCEPT
    SELECT product_id FROM production.stocks WHERE store_id=2
) AS store_diff;


----------------------------------------------------------------------------------------------------
--20.Complex set operations for customer retention:
--Find customers who bought in 2022 but not in 2023 (lost customers)
--Find customers who bought in 2023 but not in 2024 (new customers)
--Find customers who bought in both years (retained customers)
--Use UNION ALL to combine all three groups

(SELECT customer_id , 'lost' AS Statu
From sales.orders
Where YEAR (order_date)=2022
EXCEPT
SELECT customer_id , 'lost' AS Statu
From sales.orders
Where YEAR (order_date)=2023)

UNION ALL

(SELECT customer_id , 'New' AS Statu
From sales.orders
Where YEAR (order_date)=2023
EXCEPT
SELECT customer_id , 'New' AS Statu
From sales.orders
Where YEAR (order_date)=2022)

UNION ALL 

(SELECT customer_id , 'retained' AS Statu
From sales.orders
Where YEAR (order_date)=2022
INTERSECT
SELECT customer_id , 'retained' AS Statu
From sales.orders
Where YEAR (order_date)=2023)
