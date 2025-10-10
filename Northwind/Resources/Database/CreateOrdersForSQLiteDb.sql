ROLLBACK;
PRAGMA foreign_keys = ON;

-- OPTIONAL speed-ups for bulk load:
-- PRAGMA journal_mode = WAL;
-- PRAGMA synchronous = OFF;
-- BEGIN IMMEDIATE;

DROP TRIGGER IF EXISTS temp.gen_random_order_details;
CREATE TEMP TRIGGER gen_random_order_details
AFTER INSERT ON Orders
BEGIN
  INSERT INTO OrderDetails (OrderID, ProductID, UnitPrice, Quantity, Discount)
  WITH
    params AS (SELECT 10 AS min_lines, 20 AS max_lines),
    -- Adjust filter as needed (e.g., WHERE Discontinued = 0)
    product_pool AS (
      SELECT ProductID, UnitPrice
      FROM Products
      -- WHERE Discontinued = 0
    ),
    counts AS (SELECT COUNT(*) AS total FROM product_pool),
    line_bounds AS (
      SELECT
        p.min_lines,
        -- cap max_lines to available products
        MIN(p.max_lines, c.total) AS capped_max
      FROM params p, counts c
    ),
    line_count AS (
      SELECT
        CASE
          WHEN capped_max <= min_lines THEN capped_max
          ELSE ((ABS(RANDOM()) % (capped_max - min_lines + 1)) + min_lines)
        END AS k
      FROM line_bounds
    ),
    picked_products AS (
      SELECT ProductID, UnitPrice
      FROM product_pool
      ORDER BY RANDOM()
      LIMIT (SELECT k FROM line_count)
    ),
    rnd AS (
      SELECT
        ProductID,
        UnitPrice,
        (ABS(RANDOM()) % 20) + 1         AS Quantity,  -- 1..10
        (ABS(RANDOM()) % 31) / 100.0     AS Discount   -- 0.00..0.30
      FROM picked_products
    )
  SELECT
    NEW.OrderID,
    r.ProductID,
    ROUND(r.UnitPrice, 2),
    r.Quantity,
    r.Discount
  FROM rnd r;
END;

-- Bulk insert random Orders (edit LIMIT for count)
WITH RECURSIVE
seq(n) AS (
  SELECT 1
  UNION ALL
  SELECT n + 1 FROM seq
  LIMIT 50000
),
date_window AS (
  SELECT
    julianday('1975-01-01') AS start_jd,
    julianday('2030-12-31') AS end_jd
),
orders_to_insert AS (
  SELECT
    (SELECT c.CustomerID FROM Customers c ORDER BY RANDOM() LIMIT 1) AS CustomerID,
    (SELECT e.EmployeeID FROM Employees e ORDER BY RANDOM() LIMIT 1) AS EmployeeID,
    (SELECT s.ShipperID  FROM Shippers s  ORDER BY RANDOM() LIMIT 1) AS ShipVia,
    date(
      (SELECT start_jd FROM date_window)
      + (ABS(RANDOM()) % CAST(((SELECT end_jd FROM date_window) - (SELECT start_jd FROM date_window)) AS INT))
    ) AS OrderDate
  FROM seq
)
INSERT INTO Orders
  (CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate,
   ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode, ShipCountry)
SELECT
  o.CustomerID,
  o.EmployeeID,
  o.OrderDate,
  date(o.OrderDate, '+' || ((ABS(RANDOM()) % 28) + 3) || ' days') AS RequiredDate,
  CASE WHEN (ABS(RANDOM()) % 10) < 4
       THEN NULL
       ELSE date(o.OrderDate, '+' || (ABS(RANDOM()) % 11) || ' days')
  END AS ShippedDate,
  o.ShipVia,
  ROUND((ABS(RANDOM()) % 20000) / 100.0, 2) AS Freight,
  c.CompanyName,
  c.Address,
  c.City,
  c.Region,
  c.PostalCode,
  c.Country
FROM orders_to_insert o
JOIN Customers c ON c.CustomerID = o.CustomerID;

DROP TRIGGER IF EXISTS temp.gen_random_order_details;

-- OPTIONAL: restore pragmas / end txn
-- COMMIT;
-- PRAGMA synchronous = FULL;

CREATE TABLE dim_date (
    date_key         INTEGER PRIMARY KEY,   -- YYYYMMDD as integer surrogate key
    full_date        DATE NOT NULL,         -- Actual date
    day              INTEGER NOT NULL,      -- Day of the month
    month            INTEGER NOT NULL,      -- Month number (1–12)
    month_name       TEXT NOT NULL,         -- Month name
    quarter          INTEGER NOT NULL,      -- Quarter (1–4)
    year             INTEGER NOT NULL,      -- Year
    week_of_year     INTEGER NOT NULL,      -- ISO week number
    day_of_week      INTEGER NOT NULL,      -- 1=Monday … 7=Sunday
    day_name         TEXT NOT NULL,         -- Day name
    is_weekend       INTEGER NOT NULL       -- 0=weekday, 1=weekend
);
WITH RECURSIVE dates(d) AS (
    SELECT date('1975-01-01')  -- Start date (adjust as needed)
    UNION ALL
    SELECT date(d, '+1 day')
    FROM dates
    WHERE d < date('2031-01-01') -- End date (adjust as needed)
)
INSERT INTO dim_date
SELECT
    CAST(strftime('%Y%m%d', d) AS INTEGER) AS date_key,
    d AS full_date,
    CAST(strftime('%d', d) AS INTEGER) AS day,
    CAST(strftime('%m', d) AS INTEGER) AS month,
    CASE strftime('%m', d)
        WHEN '01' THEN 'January'
        WHEN '02' THEN 'February'
        WHEN '03' THEN 'March'
        WHEN '04' THEN 'April'
        WHEN '05' THEN 'May'
        WHEN '06' THEN 'June'
        WHEN '07' THEN 'July'
        WHEN '08' THEN 'August'
        WHEN '09' THEN 'September'
        WHEN '10' THEN 'October'
        WHEN '11' THEN 'November'
        WHEN '12' THEN 'December'
    END AS month_name,
    ((CAST(strftime('%m', d) AS INTEGER) - 1) / 3 + 1) AS quarter,
    CAST(strftime('%Y', d) AS INTEGER) AS year,
    CAST(strftime('%W', d) AS INTEGER) + 1 AS week_of_year,
    CAST(strftime('%w', d) AS INTEGER) + 1 AS day_of_week,
    CASE strftime('%w', d)
        WHEN '0' THEN 'Sunday'
        WHEN '1' THEN 'Monday'
        WHEN '2' THEN 'Tuesday'
        WHEN '3' THEN 'Wednesday'
        WHEN '4' THEN 'Thursday'
        WHEN '5' THEN 'Friday'
        WHEN '6' THEN 'Saturday'
    END AS day_name,
    CASE strftime('%w', d)
        WHEN '0' THEN 1
        WHEN '6' THEN 1
        ELSE 0
    END AS is_weekend
FROM dates;

CREATE VIEW vwSalesReport
AS
SELECT  
	o.OrderID AS OrderKey,
	od.ProductID AS ProductKey,
	o.EmployeeId AS EmployeeKey,
	o.CustomerId AS CustomerKey,
	o.ShipVia AS ShipperKey,
	p.SupplierID AS SupplierKey,
	p.CategoryID AS CategoryKey,
	et.TerritoryID AS EmployeeTerritoryKey,
	t.RegionID AS RegionKey,
	o.OrderDate AS OrderedAt,
	CASE WHEN o.OrderDate IS NOT NULL THEN CAST(strftime('%Y%m%d',o.OrderDate) AS INTEGER) ELSE NULL END OrderedAtKey,
	o.RequiredDate AS RequiredAt,
	CASE WHEN o.RequiredDate IS NOT NULL THEN CAST(strftime('%Y%m%d',o.RequiredDate) AS INTEGER) ELSE NULL END AS RequiredAtKey,
	o.ShippedDate AS ShippedAt,
	CASE WHEN o.ShippedDate IS NOT NULL THEN CAST(strftime('%Y%m%d', o.ShippedDate) AS INTEGER) ELSE NULL END AS ShippedAtKey,
	od.UnitPrice,
	od.Quantity,
	od.Discount AS DiscountPercentage,
	(od.UnitPrice * od.Quantity) AS SalesPrice,
	(od.UnitPrice * od.Quantity) * od.Discount AS DiscountAmount,
	(od.UnitPrice * od.Quantity) - (od.UnitPrice * od.Quantity) * od.Discount AS EndSalesPrice,
	o.ShipCountry AS Country,
	o.ShipRegion AS Region,
	o.ShipCity AS City,
	o.ShipPostalCode AS ZipCode
FROM 
	OrderDetails AS od
LEFT JOIN Orders AS o
ON
	od.OrderID = o.OrderID
LEFT JOIN Products p
ON
	od.ProductID = p.ProductID
LEFT JOIN Employees e
ON
	o.EmployeeId = e.EmployeeId
LEFT JOIN EmployeeTerritories et
ON
	et.EmployeeID = e.EmployeeID
LEFT JOIN Territories t
ON
	t.TerritoryID = et.TerritoryID

--LIMIT 1000