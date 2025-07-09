---------------------------------------------------- Assignment 2 TASK 1 ------------------------------------------
--CREATE DATABASE CO;
--USE CO;
-------------------------------------- CREATE AND INSERT DATA ----------------------------------------
CREATE TABLE Department (
DeptID INT PRIMARY KEY IDENTITY(1,1) ,
Deptname VARCHAR(255) NOT NULL,
);
-----------------------------------------------------
CREATE TABLE Employee (
SSN INT PRIMARY KEY ,
F_name VARCHAR(255) NOT NULL, 
l_name VARCHAR(255) NOT NULL, 
gender CHAR(1) CHECK(gender in('M','F')) NOT NULL,
Salary DECIMAL(10,2) DEFAULT 5000 CHECK (salary>0), 
DeptID INT NOT NULL,
FOREIGN KEY(DeptID) REFERENCES Department(DeptID)
);
----------------------------------------------------
CREATE TABLE Dependent (
EmpSSN INT NOT NULL,
DependentName VARCHAR(255) NOT NULL, 
gender CHAR(1) CHECK(gender in('M','F')) NOT NULL,
Relation VARCHAR(255),
PRIMARY KEY(EmpSSN,DependentName),
FOREIGN KEY(EmpSSN) REFERENCES Employee(SSN)
ON DELETE CASCADE
);
----------------------------------------------------
CREATE TABLE Project (
ProjID INT PRIMARY KEY IDENTITY(1,1) ,
ProjName VARCHAR(255) NOT NULL,
DeptID INT NOT NULL,
FOREIGN KEY(DeptID) REFERENCES Department(DeptID)
ON DELETE CASCADE
ON UPDATE CASCADE
);
-----------------------------------------------
CREATE TABLE Employee_Project(
EmpSSN INT NOT NULL,
ProjID INT NOT NULL,
Hours_Worked DECIMAL(5,2) Default 1 check(Hours_Worked>0)
PRIMARY KEY (EmpSSN,ProjID),
FOREIGN KEY(ProjID) REFERENCES Project(ProjID)
ON DELETE CASCADE
ON UPDATE CASCADE,
FOREIGN KEY(EmpSSN) REFERENCES Employee(SSN)
ON DELETE CASCADE
ON UPDATE CASCADE
);

-- Add FK constraint for ManagerID from Employee table 

ALTER TABLE Department ADD ManagerID INT ;
---------------------  ADD FOREIGN KEY Constraint ---------------------------------
ALTER TABLE Department
ADD CONSTRAINT FK_Department_Manager
FOREIGN KEY (ManagerID) REFERENCES Employee(SSN);
-------------------------------------------------------
INSERT INTO Department (Deptname) VALUES 
('HR'),
('IT'),
('Finance'),
('Marketing'),
('Logistics');
----------------------------------------------------------------------------
INSERT INTO Employee (SSN, F_name, l_name, gender, Salary, DeptID) VALUES 
(1001, 'Ali', 'Hassan', 'M', 7000, 1),
(1002, 'Mona', 'Youssef', 'F', 6500, 2),
(1003, 'Omar', 'Gamal', 'M', 6000, 3),
(1004, 'Sara', 'Mostafa', 'F', 5500, 2),
(1005, 'Nour', 'Saeed', 'F', 6200, 1),
(1006, 'Kareem', 'Samir', 'M', 6800, 4),
(1007, 'Laila', 'Ibrahim', 'F', 5900, 4),
(1008, 'Hani', 'Fouad', 'M', 6400, 5),
(1009, 'Nada', 'Kamel', 'F', 5300, 3),
(1010, 'Tamer', 'Zaki', 'M', 6100, 5);
----------------------------------------------------------------------------------
UPDATE Department SET ManagerID = 1001 WHERE DeptID = 1;
UPDATE Department SET ManagerID = 1002 WHERE DeptID = 2;
UPDATE Department SET ManagerID = 1003 WHERE DeptID = 3;
UPDATE Department SET ManagerID = 1006 WHERE DeptID = 4;
UPDATE Department SET ManagerID = 1008 WHERE DeptID = 5;
----------------------------------------------------------------------------
INSERT INTO Dependent (EmpSSN, DependentName, gender, Relation) VALUES 
(1001, 'Lina', 'F', 'Daughter'),
(1001, 'Youssef', 'M', 'Son'),
(1002, 'Tarek', 'M', 'Son'),
(1005, 'Mariam', 'F', 'Daughter'),
(1008, 'Adam', 'M', 'Son'),
(1010, 'Aya', 'F', 'Daughter');

INSERT INTO Project (ProjName, DeptID) VALUES 
('Payroll System', 3),
('Recruitment Campaign', 1),
('Inventory Management', 2),
('Employee Portal', 2),
('Budget Planner', 3),
('Marketing Strategy', 4),
('Fleet Tracking', 5);
------------------------------------------------------------------------
INSERT INTO Employee_Project (EmpSSN, ProjID, Hours_Worked) VALUES
(1001, 2, 10.5),
(1002, 3, 12.0),
(1003, 1, 15.0),
(1004, 4, 8.0),
(1005, 2, 7.5),
(1002, 4, 9.0),
(1003, 5, 14.0),
(1006, 6, 10.0),
(1007, 6, 8.5),
(1008, 7, 11.0),
(1009, 1, 6.5),
(1010, 7, 9.0);
---------------------------------- ADD NEW COLUMN ---------------------------------------------

ALTER TABLE Employee ADD Email Varchar(255);

UPDATE Employee SET Email = 'ali.hassan@company.com' WHERE SSN = 1001;
UPDATE Employee SET Email = 'mona.youssef@company.com' WHERE SSN = 1002;
UPDATE Employee SET Email = 'omar.gamal@company.com' WHERE SSN = 1003;
UPDATE Employee SET Email = 'sara.mostafa@company.com' WHERE SSN = 1004;
UPDATE Employee SET Email = 'nour.saeed@company.com' WHERE SSN = 1005;
UPDATE Employee SET Email = 'kareem.samir@company.com' WHERE SSN = 1006;
UPDATE Employee SET Email = 'laila.ibrahim@company.com' WHERE SSN = 1007;
UPDATE Employee SET Email = 'hani.fouad@company.com' WHERE SSN = 1008;
UPDATE Employee SET Email = 'nada.kamel@company.com' WHERE SSN = 1009;
UPDATE Employee SET Email = 'tamer.zaki@company.com' WHERE SSN = 1010;

------------------------------------------ Add Constrain ---------------------------------------
 ALTER TABLE Employee ADD Constraint  UQ_Employee_Email UNIQUE(Email);
 -- UPDATE Employee SET Email = 'tamer.zaki@company.com' WHERE SSN = 1001; -- check for My new constraint ??

 --------------------------------------- Modifying a column's data type --------------------------
 ALTER TABLE Employee ALTER COLUMN F_name NVARCHAR(255);   -- done ??

 -------------------------------------------------- let' Drop This Email Constraint -------------------
  ALTER TABLE Employee DROP Constraint  UQ_Employee_Email;

