CREATE TABLE Department (
    Dnum INT PRIMARY KEY,
    Dname VARCHAR(100) NOT NULL UNIQUE
);
CREATE TABLE Employee (
    SSN int PRIMARY KEY,
    F_name VARCHAR(100) NOT NULL,
    L_name VARCHAR(100) NOT NULL,
    Gender CHAR(1) NOT NULL CHECK (Gender IN ('M', 'F')),
    Birth_Date DATE NOT NULL,
    Dnum INT NOT NULL,
    SupervisorSSN int,
    FOREIGN KEY (Dnum) REFERENCES Department(Dnum),
    FOREIGN KEY (SupervisorSSN) REFERENCES Employee(SSN)
);
CREATE TABLE Department_Locations (
    Dnum INT NOT NULL,
    Location VARCHAR(100) NOT NULL,
    PRIMARY KEY (Dnum, Location),
    FOREIGN KEY (Dnum) REFERENCES Department(Dnum)
);
CREATE TABLE Project(
   PNumber INT PRIMARY KEY,
   Pname   VARCHAR(100) NOT NULL,
   City   VARCHAR(100) NOT NULL,
    Dnum INT NOT NULL,
   FOREIGN KEY (Dnum) REFERENCES Department(Dnum), 

);
CREATE TABLE WORK_IN (
    SSN INT NOT NULL,
	PNumber INT NOT NULL,
	Hours DECIMAL(4,1) CHECK (Hours >= 0),
	PRIMARY KEY (SSN, PNumber),
    FOREIGN KEY (SSN) REFERENCES Employee(SSN),
    FOREIGN KEY (PNumber) REFERENCES Project(PNumber)
);
CREATE TABLE Dependent(
	SSN INT NOT NULL,
	Dependent_Name VARCHAR(100) NOT NULL,
	Gender CHAR(1) NOT NULL CHECK (Gender IN ('M', 'F')),
    Birth_Date DATE NOT NULL,
	PRIMARY KEY (SSN, Dependent_Name),
	FOREIGN KEY (SSN) REFERENCES Employee(SSN),
);

Create Table Manage(
    Dnum INT PRIMARY KEY,
    SSN INT NOT NULL,
    Hire_Date DATE NOT NULL,
    FOREIGN KEY (Dnum) REFERENCES Department(Dnum),
    FOREIGN KEY (SSN) REFERENCES Employee(SSN)
);

INSERT INTO Department (Dnum, Dname) VALUES
(1, 'HR'),
(2, 'IT'),
(3, 'Finance');


INSERT INTO Employee (SSN, F_name, L_name, Gender, Birth_Date, Dnum, SupervisorSSN) VALUES
(1, 'Alyaa', 'Gamal', 'F', '1998-01-01', 2, NULL),
(2, 'Maryam', 'Tarek', 'F', '1995-03-15', 2, 1),
(3, 'Yasmine', 'Raef', 'F', '1997-06-22', 1, 1),
(4, 'Youssef', 'Gamal', 'M', '1993-12-05', 3, 2),
(5, 'Menna', 'Fathy', 'F', '1999-09-30', 1, 2);


INSERT INTO Project (PNumber, Pname, City, Dnum) VALUES
(1, 'Website Upgrade', 'Cairo', 2),
(2, 'Payroll System', 'Alex', 3),
(3, 'Training Portal', 'Cairo', 1);
 
 INSERT INTO WORK_IN(SSN, PNumber, Hours) VALUES
(1, 1, 20),
(2, 1, 35),
(3, 2, 15),
(4, 3, 25),
(5, 2, 40);

UPDATE Employee
SET Dnum = 1
WHERE SSN = 1;

INSERT INTO Dependent (SSN, Dependent_Name, Gender, Birth_Date) VALUES
(1, 'Maryam', 'F', '2012-05-10'),
(2, 'Ali', 'M', '2015-07-22'),
(3, 'Layla', 'F', '2010-03-30'),
(4, 'Kareem', 'M', '2014-11-12'),
(5, 'Nada', 'F', '2013-01-05');
 
DELETE FROM Dependent
WHERE SSN = 5 AND Dependent_Name = 'Nada';

SELECT F_name +' '+ L_name as Name
FROM Employee 
WHERE Dnum = 1 ;

SELECT 
    SSN,
    (SELECT F_name+' '+L_name as Name FROM Employee WHERE Employee.SSN = WORK_IN.SSN) AS Name,
    (SELECT Pname FROM Project WHERE Project.PNumber = WORK_IN.PNumber) AS "Project Name",
    Hours
FROM WORK_IN;




